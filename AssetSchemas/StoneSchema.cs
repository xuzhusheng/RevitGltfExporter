using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class StoneSchema : IAssetSchema
    {
        public string colorByObjectKey => "stone_color_by_object";

        public string diffuseKey => "stone_color";

        public string diffuseImageFadeKey => "stone_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "stone_reflectivity_at_90deg";

        public string glossinessKey => "refl_gloss";

        public string isMetalKey => "stone_is_metal";

        public string transparencyKey => "stone_transparency";

        public string transparencyImageFadeKey => "stone_transparency_image_fade";

        public string refractionIndexKey => "stone_refraction_index";

        public string refractionTranslucencyWeightKey => "stone_refraction_translucency_weight";

        public string cutoutOpacityKey => "stone_cutout_opacity";

        public string backfaceCullKey => "stone_backface_cull";

        public string selfIllumLuminanceKey => "stone_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "stone_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "stone_refraction_glossy_samples";

        public string aoOnKey => "stone_ao_on";

        public string aoSamplesKey => "stone_ao_samples";

        public string aoDistanceKey => "stone_ao_distance";

        public string aoDetailsKey => "stone_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "stone_roundcorners_allow_different_materials";

        public string reflDepthKey => "stone_refl_depth";

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

        //<boolean name = "stone_color_by_object"                val="false"/>
        //<float name = "stone_diffuse_image_fade"             val="1."/>
        //<float name = "stone_reflectivity_at_90deg"          val="1."/>
        //<boolean name = "stone_is_metal"                       val="false"/>
        //<float name = "stone_transparency"                   val="0.0"/>
        //<float name = "stone_transparency_image_fade"        val="1."/>
        //<float name = "stone_refraction_index"               val="1.4"/>
        //<float name = "stone_refraction_translucency_weight" val=".5"/>
        //<float name = "stone_cutout_opacity"                 val="1."/>
        //<boolean name = "stone_backface_cull"                  val="false"/>
        //<float name = "stone_self_illum_luminance"           val="0"/>
        //<float name = "stone_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "stone_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.colorByObject = false;
            material.diffuseImageFade = 1;
            material.reflectivityAt90deg = 1.0f;
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