using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class MetalSchema : IAssetSchema
    {
        public string colorByObjectKey => "metal_color_by_object";

        //重复Key
        //<map_operator source = "diffuse_color"                        destination="generic_diffuse"/>
        //<map_bindings source = "diffuse_asset"                        destination="generic_diffuse"/>
        public string diffuseKey => "diffuse_color";
        //public string diffuseKey => "diffuse_asset";

        public string diffuseImageFadeKey => "metal_diffuse_image_fade";

        //重复Key
        //<map_operator source = "reflectivity_at_0deg"                 destination="generic_reflectivity_at_0deg"/>
        //<map_bindings source = "reflectivity_at_0deg_asset"           destination="generic_reflectivity_at_0deg"/>
        public string reflectivityAt0degKey => "reflectivity_at_0deg";
        //public string reflectivityAt0degKey => "reflectivity_at_0deg_asset";

        public string reflectivityAt90degKey => "metal_reflectivity_at_90deg";

        public string glossinessKey => "refl_gloss";

        public string isMetalKey => "is_metal";

        public string transparencyKey => "metal_transparency";

        public string transparencyImageFadeKey => "metal_transparency_image_fade";

        public string refractionIndexKey => "metal_refraction_index";

        public string refractionTranslucencyWeightKey => "metal_refraction_translucency_weight";

        public string cutoutOpacityKey => "cutout_opacity_asset";

        public string backfaceCullKey => "metal_backface_cull";

        public string selfIllumLuminanceKey => "metal_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "metal_self_illum_color_temperature";

        public string bumpAmountKey => "metal_pattern_height";

        public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "metal_refraction_glossy_samples";

        public string aoOnKey => "metal_ao_on";

        public string aoSamplesKey => "metal_ao_samples";

        public string aoDistanceKey => "metal_ao_distance";

        public string aoDetailsKey => "metal_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "metal_roundcorners_allow_different_materials";

        public string reflDepthKey => "metal_refl_depth";

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

        //<float name = "metal_diffuse_image_fade"             val="1."/>
        //<float name = "metal_reflectivity_at_90deg"          val="1."/>
        //<float name = "metal_transparency"                   val="0.0"/>
        //<float name = "metal_transparency_image_fade"        val="1."/>
        //<float name = "metal_refraction_index"               val="1.4"/>
        //<float name = "metal_refraction_translucency_weight" val=".5"/>
        //<boolean name = "metal_backface_cull"                  val="false"/>
        //<float name = "metal_self_illum_luminance"           val="0.0"/>
        //<float name = "metal_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "metal_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt90deg = 1;
            material.transparency = 0;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.refractionGlossySamples = 1;
        }
    }
}