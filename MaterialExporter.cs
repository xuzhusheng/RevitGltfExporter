using Autodesk.Revit.DB;
#if REVIT2016 || REVIT2017
using Autodesk.Revit.Utility;
//#elif REVIT2018
//using Autodesk.Revit.DB.Visual;
#else
using Autodesk.Revit.DB.Visual;
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


//mtl 文件使用的关键字
//newmtl: 定义新的材质组，后面参数为材质组名称

//Ka: 环境反射（ambient color）
//有三种描述格式，三者是互斥的，不能同时使用
//*Ka r g b 用RGB颜色值用来表示，g、b是可选的，如果只指定了r的值，则g、b的值都等于r的值。三个参数一般取值范围0.0~1.0，在此范围外的值则相应的增加或减少反射率;

//Ka spectral file.rfl factor 用一个rfl文件表示。factor是一个可选参数，表示.rfl文件中值的乘数，默认1.0；
//Ka xyz x y z 用CIEXYZ值来表示，x、y、z是CIEXYZ颜色空间的各分量值。y和z两参数是可选的，如果只指定了x的值，则y和z的值等于r的值。三个参数一般取值范围为0.0~1.0。
//Kd: 漫反射（diffuse color）

//Kd r g b
//Kd spectral file.rfl factor
//Kd xyz x y z
//Ks: 镜反射（specular color）

//Ks r g b
//Ks spectral file.rfl factor
//Ks xyz x y z
//Tf: 滤光透射率（specular color）

//Tf r g b
//Tf spectral file.rfl factor
//Tf xyz x y z
//Ke: 放射光（emissive color）

//Ke r g b
//Ke spectral file.rfl factor
//Ke xyz x y z
//illum： 照明度（illumination）
//illum num num取值范围0~10.

//值	意义
//0	Color on and Ambient off
//1	Color on and Ambient on
//2	Highlight on
//3	Reflection on and Ray trace on
//4	Transparency: Glass on / Reflection: Ray trace on
//5	Reflection: Fresnel on and Ray trace on
//6	Transparency: Refraction on / Reflection: Fresnel off and Ray trace on
//7	Transparency: Refraction on / Reflection: Fresnel on and Ray trace on
//8	Reflection on and Ray trace off
//9	Transparency: Glass on / Reflection: Ray trace off
//10	Casts shadows onto invisible surfaces
//d，渐隐指数

//d factor 表示物体融入背景的数量，取值范围0.0~·1.0，不写默认1.0（不透明），与真正的透明物体材质不一样，这个渐隐效果是不依赖于物体的厚度或者是否具有光谱特性。该渐隐效果对所有光照模型都有效。0完全透明；1 完全不透明。
//d -halo factor 指定一种受观察者影响的渐隐效果。列如，对于定义一个d -halo 0 的球体，在它的中心是完全消隐的，而在表面边界将逐渐变得不透明。其中factor表示应用在材质上的渐隐率的最小值。而材质上具体的渐隐率将在这个最小值到1.0之间取值。其计算公式为：dissolve = 1.0 - (N * v)(1.0 - factor)
//Ns: 反射指数
//Ns exponent指定材质的反射指数，定义了反射高光度。
//exponent是反射指数值，该值越高则高光越密集，一般取值范围在0 ~1000。

//Sharpness： 清晰度（sharpness）
//Sharpness value 指定本地反射贴图的清晰度。如果材质中没有本地反射贴图定义，则将此值应用到预览中的全局反射贴图上。
//value可在0~1000中取值，默认60。值越高则越清晰。

//Ni : 折射值描述（optical density）
//Ni ptical density指定材质表面的光密度，既折射值。
//ptical density是光密度值，可在0.001到10之间进行取值。若取值为1.0，光在通过物体的时候不发生弯曲。玻璃的折射率为1.5。取值小于1.0的时候可能回产生奇怪的结果，不推荐。

//Tr 用于定义材质的Alpha透明度

//Tf: 材质的透射滤波（transmission filter），对应数据为r、g、b值
//map_Ka、map_Kd、 map_Ks 材质的环境，散射和镜面贴图，对应数据为贴图文件名称
//refl: 材质的反射属性
//纹理映射
//纹理映射可以对映射的相应材质参数进行修改，这个修改只是对原有存在的参数进行叠加修改，而不是替换原有参数，从而纹理映射在物体表面的表现上有很好的灵活性。

//纹理映射只可以改变以下材质参数：

//- Ka (color)
//-Kd(color)
//- Ks(color)
//- Ns(color)
//- d(scalar)
//除以上参数外，表面法线也可以更改.

//纹理文件可以有以下几种类型：

//纹理映射文件
//.mpc: 颜色纹理文件 color texture files -- 可以改变Ka，Kd，Ks的值
//.mps: 标量纹理文件 scalar texture files -- 可改变Ns, d, decal的值
//.mpb: 凹凸纹理文件 bump texture files -- 可改变面法线
//程序纹理文件
//程序纹理文件是用数学公式计算纹理的样本值。有以下几种格式：

//.cxc
//.cxs
//.cxb
//以下是mtl文件中对于纹理映射的描述格式：

//map_Ka -options args filename
//为环境反射指定颜色纹理文件(.mpc)或程序纹理文件(.cxc)或是一个位图文件（jpg、png等）。
//在渲染的时候，Ka的值将再乘上map_Ka的值。
//而map_Ka的可选项参数有以下几个：

//-blendu on | off
//- blendv on | off
// - cc on | off
//  - clamp on | off
//   - mm bse gain
//-o u v w
//-s u v w
//-t u v w
//-texres value
//map_Kd -options args filename
//为漫反射指定颜色纹理文件(.mpc)或程序纹理文件(.cxc)或是一个位图文件（jpg、png等）。
//作用原理与可选参数和map_Ka同。

//map_Ks - options args filename
//为镜反射指定颜色纹理文件(.mpc)或程序纹理文件(.cxc)或是一个位图文件（jpg、png等）。
//作用原理与可选参数和map_Ka同。


// materials
//Slot Metallic roughness	Specular glossiness 
//Ka	occlusion value	occlusion value
//Ke	emissive color	emissive color
//Kd	base color	diffuse color
//Ks	metallic value	specular color
//Ns	roughness value	glossiness value
//d	alpha	alpha
//Tr	1.0 - alpha	1.0 - alpha
//map_Ka	occlusion texture	occlusion texture
//map_Ke	emissive texture	emissive texture
//map_Kd	base color texture	diffuse texture
//map_Ks	metallic texture	specular texture
//map_Ns	roughness texture	glossiness texture
//map_Bump	normal texture	normal texture

//巨坑， 慎入
//C:\Program Files (x86)\Common Files\Autodesk Shared\Materials\2016\assetlibrary_base.fbm\Mats 目录下有各种材质 schema 对应值 可做映射
// rendering appreance properties manual
//https://knowledge.autodesk.com/support/revit/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/Revit-Customize/files/GUID-AD16422E-B48C-47EC-A335-56D526B52089-htm.html
namespace RevitGltfExporter
{
    partial class MaterialExporter
    {
        static public int export(Document doc, MaterialNode node)
        {
            if (node.MaterialId.IntegerValue == -1) return -1;

            int index = Gltf.Instance.getMaterialIndex(node.MaterialId.IntegerValue);
            if (-1 != index) return index;
            
            //Material material = new Material(node.MaterialId.IntegerValue);
            var revitmaterial = doc.GetElement(node.MaterialId);
            string name = revitmaterial != null ? revitmaterial.Name : node.NodeName;
            //double[] color = new double[] { node.Color.Red / 255.0, node.Color.Green / 255.0, node.Color.Blue / 255.0 };
            RenderingMaterial renderingMaterial = new RenderingMaterial(node.MaterialId.IntegerValue, name, new double[] { node.Color.Red / 255.0, node.Color.Green / 255.0, node.Color.Blue / 255.0 }, node.Transparency);
            Asset asset = node.HasOverriddenAppearance ? node.GetAppearanceOverride() : node.GetAppearance();
            renderingMaterial.parseAsset(asset);
            return Gltf.Instance.add(renderingMaterial);
        }
    }

}
