using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class CeramicSchema : IAssetSchema
    {
        public string colorByObjectKey => "ceramic_color_by_object";

        public string diffuseKey => "ceramic_color";

        public string diffuseImageFadeKey => "ceramic_diffuse_image_fade";

        public string reflectivityAt0degKey => "reflectivity_at_0deg";

        public string reflectivityAt90degKey => "reflectivity_at_90deg";

        public string glossinessKey => "refl_gloss";

        public string isMetalKey => "ceramic_is_metal";

        public string transparencyKey => "ceramic_transparency";

        public string transparencyImageFadeKey => "ceramic_transparency_image_fade";

        public string refractionIndexKey => "ceramic_refraction_index";

        public string refractionTranslucencyWeightKey => "ceramic_refraction_translucency_weight";

        public string cutoutOpacityKey => "ceramic_cutout_opacity";

        public string backfaceCullKey => "ceramic_backface_cull";

        public string selfIllumLuminanceKey => "ceramic_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "ceramic_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "ceramic_refraction_glossy_samples";

        public string aoOnKey => "ceramic_ao_on";

        public string aoSamplesKey => "ceramic_ao_samples";

        public string aoDistanceKey => "ceramic_ao_distance";

        public string aoDetailsKey => "ceramic_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "ceramic_roundcorners_allow_different_materials";

        public string reflDepthKey => "ceramic_refl_depth";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

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

        //<float name = "ceramic_diffuse_image_fade"             val="1.0"/>
        //<boolean name = "ceramic_is_metal"                       val="false"/>
        //<float name = "ceramic_transparency"                   val="0.0"/>
        //<float name = "ceramic_transparency_image_fade"        val="1.0"/>
        //<float name = "ceramic_refraction_index"               val="1.4"/>
        //<float name = "ceramic_refraction_translucency_weight" val="0.5"/>
        //<float name = "ceramic_cutout_opacity"                 val="1.0"/>
        //<boolean name = "ceramic_backface_cull"                  val="false"/>
        //<float name = "ceramic_self_illum_luminance"           val="0"/>
        //<float name = "ceramic_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "ceramic_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.isMetal = false;
            material.transparency = 0;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.refractionGlossySamples = 1;
        }
    }
}