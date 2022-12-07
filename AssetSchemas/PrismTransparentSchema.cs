using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class PrismTransparentSchema : IAssetSchema
    {
        public string colorByObjectKey => "prism_transparent_color_by_object";

        public string diffuseKey => "transparent_color";

        public string diffuseImageFadeKey => "prism_transparent_diffuse_image_fade";

        public string reflectivityAt0degKey => "prism_transparent_reflectivity_at_0deg";

        public string reflectivityAt90degKey => "prism_transparent_reflectivity_at_90deg";

        public string glossinessKey => "glossiness";

        public string isMetalKey => "prism_transparent_is_metal";

        public string transparencyKey => "transparency";

        public string transparencyImageFadeKey => "prism_transparent_transparency_image_fade";

        public string refractionIndexKey => "transparent_ior";

        public string refractionTranslucencyWeightKey => "prism_transparent_refraction_translucency_weight";

        public string cutoutOpacityKey => "cutout_opacity_map";

        public string backfaceCullKey => "prism_transparent_backface_cull";

        public string selfIllumLuminanceKey => "prism_transparent_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "prism_transparent_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string commonTintToggleKey => "prism_transparent_common_Tint_toggle";

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

        //boolean name = "prism_transparent_color_by_object"                val="false"/>
        //<float name = "prism_transparent_diffuse_image_fade"             val="1."/>
        //<float name = "prism_transparent_reflectivity_at_0deg"           val=".1"/>
        //<float name = "prism_transparent_reflectivity_at_90deg"          val=".5"/>
        //<boolean name = "prism_transparent_is_metal"                       val="false"/>
        //<float name = "prism_transparent_transparency_image_fade"        val="0.0"/>
        //<float name = "prism_transparent_refraction_index"               val="1.4"/>
        //<float name = "prism_transparent_refraction_translucency_weight" val=".5"/>
        //<boolean name = "prism_transparent_backface_cull"                  val="false"/>
        //<float name = "prism_transparent_self_illum_luminance"           val="0.0"/>
        //<float name = "prism_transparent_self_illum_color_temperature"   val="0.0"/>
        //<boolean name = "prism_transparent_common_Tint_toggle"             val="false"/>

        public void setDefault(RenderingMaterial material)
        {
            material.colorByObject = false;
            material.diffuseImageFade = 1;
            material.reflectivityAt0deg=0.1f;
            material.reflectivityAt90deg = 0.5f;
            material.isMetal = false;
            material.transparencyImageFade = 0.0f;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.commonTintToggle = false;
        }
    }
}