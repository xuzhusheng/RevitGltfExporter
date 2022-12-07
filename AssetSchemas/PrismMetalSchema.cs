using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class PrismMetalSchema : IAssetSchema
    {
        public string colorByObjectKey => "prism_metal_color_by_object";

        //重复Key
        //<map_bindings source = "diffuse_map"                                destination="generic_diffuse"/>
        //<map_operator source = "diffuse_color"                              destination="generic_diffuse"/>
        public string diffuseKey => "diffuse_map";
        //public string diffuseKey => "diffuse_color";

        public string diffuseImageFadeKey => "diffuse_image_fade";

        public string reflectivityAt0degKey => "reflectivity_normal";

        public string reflectivityAt90degKey => "prism_metal_reflectivity_at_90deg";

        public string glossinessKey => "glossiness";

        public string isMetalKey => "prism_metal_is_metal";

        public string transparencyKey => "prism_metal_transparency";

        public string transparencyImageFadeKey => "prism_metal_transparency_image_fade";

        public string refractionIndexKey => "prism_metal_refraction_index";

        public string refractionTranslucencyWeightKey => "prism_metal_refraction_translucency_weight";

        public string cutoutOpacityKey => "cutout_opacity_map";

        public string backfaceCullKey => "prism_metal_backface_cull";

        public string selfIllumLuminanceKey => "prism_metal_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "prism_metal_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string commonTintToggleKey => "prism_metal_common_Tint_toggle";

        public string selfIllumFilterMapKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string refrDepthKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string reflectionGlossySamplesKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string refractionGlossySamplesKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string aoOnKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string aoSamplesKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string aoDistanceKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string aoDetailsKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string roundcornersRadiusKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string roundcornersAllowDifferentMaterialsKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string reflDepthKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string commonTintColorKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        //   <boolean name = "prism_metal_color_by_object"                val="false"/>
        //   <float name = "prism_metal_reflectivity_at_90deg"          val="0.0"/>
        //   <boolean name = "prism_metal_is_metal"                       val="true"/>
        //   <float name = "prism_metal_transparency"                   val="0.0"/>
        //   <float name = "prism_metal_transparency_image_fade"        val="0.0"/>
        //   <float name = "prism_metal_refraction_index"               val="1.4"/>
        //   <float name = "prism_metal_refraction_translucency_weight" val=".5"/>
        //   <boolean name = "prism_metal_backface_cull"                  val="false"/>
        //   <float name = "prism_metal_self_illum_luminance"           val="0.0"/>
        //   <float name = "prism_metal_self_illum_color_temperature"   val="0.0"/>
        //<boolean name = "prism_metal_common_Tint_toggle"             val="false"/>
        
        public void setDefault(RenderingMaterial material)
        {
            material.colorByObject = false;
            material.reflectivityAt90deg = 0.0f;
            material.isMetal = true;
            material.transparency = 0;
            material.transparencyImageFade = 0;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.commonTintToggle = false;
        }
    }
}