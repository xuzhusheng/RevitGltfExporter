using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class PlasticVinylSchema : IAssetSchema
    {
        public string colorByObjectKey => "plasticvinyl_color_by_object";

        public string diffuseKey => "plasticvinyl_color";

        public string diffuseImageFadeKey => "plasticvinyl_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "plasticvinyl_reflectivity_at_90deg";

        public string glossinessKey => "refr_gloss";

        public string isMetalKey => "plasticvinyl_is_metal";

        public string transparencyKey => "transparency";

        public string transparencyImageFadeKey => "plasticvinyl_transparency_image_fade";

        public string refractionIndexKey => "plasticvinyl_refraction_index";

        public string refractionTranslucencyWeightKey => "refr_translucency";

        public string cutoutOpacityKey => "plasticvinyl_cutout_opacity";

        public string backfaceCullKey => "plasticvinyl_backface_cull";

        public string selfIllumLuminanceKey => "plasticvinyl_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "plasticvinyl_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "refraction_glossy_samples";

        public string aoOnKey => "plasticvinyl_ao_on";

        public string aoSamplesKey => "plasticvinyl_ao_samples";

        public string aoDistanceKey => "plasticvinyl_ao_distance";

        public string aoDetailsKey => "plasticvinyl_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "plasticvinyl_roundcorners_allow_different_materials";

        public string reflDepthKey => "plasticvinyl_refl_depth";

        public string refrDepthKey => "plasticvinyl_refr_depth";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

        public string selfIllumFilterMapKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //<float name = "plasticvinyl_diffuse_image_fade"           val="1."/>
        //<float name = "plasticvinyl_reflectivity_at_90deg"        val="1."/>
        //<boolean name = "plasticvinyl_is_metal"                     val="false"/>
        //<float name = "plasticvinyl_transparency_image_fade"      val="1."/>
        //<float name = "plasticvinyl_refraction_index"             val="1.4"/>
        //<float name = "plasticvinyl_cutout_opacity"               val="1."/>
        //<boolean name = "plasticvinyl_backface_cull"                val="false"/>
        //<float name = "plasticvinyl_self_illum_luminance"         val="0.0"/>
        //<float name = "plasticvinyl_self_illum_color_temperature" val="0.0"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt90deg = 1.0f;
            material.isMetal = false;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1.4f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
        }
    }
}