using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class MasonryCMUSchema : IAssetSchema
    {
        public string colorByObjectKey => "masonrycmu_color_by_object";

        public string diffuseKey => "masonrycmu_color";

        public string diffuseImageFadeKey => "masonrycmu_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "masonrycmu_reflectivity_at_90deg";

        public string glossinessKey => "refl_gloss";

        public string isMetalKey => "masonrycmu_is_metal";

        public string transparencyKey => "masonrycmu_transparency";

        public string transparencyImageFadeKey => "masonrycmu_transparency_image_fade";

        public string refractionIndexKey => "masonrycmu_refraction_index";

        public string refractionTranslucencyWeightKey => "masonrycmu_refraction_translucency_weight";

        public string cutoutOpacityKey => "masonrycmu_cutout_opacity";

        public string backfaceCullKey => "masonrycmu_backface_cull";

        public string selfIllumLuminanceKey => "masonrycmu_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "masonrycmu_self_illum_color_temperature";

        public string bumpAmountKey => "masonrycmu_pattern_height";

        public string bumpMapKey => "masonrycmu_pattern_map";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "masonrycmu_refraction_glossy_samples";

        public string aoOnKey => "masonrycmu_ao_on";

        public string aoSamplesKey => "masonrycmu_ao_samples";

        public string aoDistanceKey => "masonrycmu_ao_distance";

        public string aoDetailsKey => "masonrycmu_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "masonrycmu_roundcorners_allow_different_materials";

        public string reflDepthKey => "masonrycmu_refl_depth";

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

        //<float name = "masonrycmu_diffuse_image_fade"             val="1."/>
        //<float name = "masonrycmu_reflectivity_at_90deg"          val="0.5"/>
        //<boolean name = "masonrycmu_is_metal"                       val="false"/> 
        //<float name = "masonrycmu_transparency"                   val="0.0"/>
        //<float name = "masonrycmu_transparency_image_fade"        val="0.0"/>
        //<float name = "masonrycmu_refraction_index"               val="1.4"/>
        //<float name = "masonrycmu_refraction_translucency_weight" val="0.5"/>
        //<float name = "masonrycmu_cutout_opacity"                 val="1.0"/>
        //<boolean name = "masonrycmu_backface_cull"                  val="false"/>
        //<float name = "masonrycmu_self_illum_luminance"           val="0.0"/>
        //<float name = "masonrycmu_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "masonrycmu_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt90deg = 0.5f;
            material.isMetal = false;
            material.transparency = 0;
            material.transparencyImageFade = 0;
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