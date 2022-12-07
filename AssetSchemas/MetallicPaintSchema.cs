using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class MetallicPaintSchema : IAssetSchema
    {
        public string colorByObjectKey => "metallicpaint_pearl_color_by_object";

        //重复Key
        //<map_operator source = "diffuse_color"                        destination="generic_diffuse"/>
        //<map_bindings source = "diffuse_asset"                        destination="generic_diffuse"/>
        public string diffuseKey => "metallicpaint_base_color";
        //public string diffuseKey => "diffuse";

        public string diffuseImageFadeKey => "metallicpaint_diffuse_image_fade";

        public string reflectivityAt0degKey => "reflectivity_at_0deg";

        public string reflectivityAt90degKey => "reflectivity_at_90deg";

        public string glossinessKey => "glossiness";

        public string isMetalKey => "metallicpaint_is_metal";

        public string transparencyKey => "metallicpaint_transparency";

        public string transparencyImageFadeKey => "metallicpaint_transparency_image_fade";

        public string refractionIndexKey => "metallicpaint_pearl_ior";

        public string cutoutOpacityKey => "metallicpaint_cutout_opacity";

        public string backfaceCullKey => "metallicpaint_backface_cull";

        public string selfIllumLuminanceKey => "metallicpaint_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "metallicpaint_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "metallicpaint_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "metallicpaint_refraction_glossy_samples";

        public string aoOnKey => "metallicpaint_ao_on";

        public string aoSamplesKey => "metallicpaint_ao_samples";

        public string aoDistanceKey => "metallicpaint_ao_distance";

        public string aoDetailsKey => "metallicpaint_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "metallicpaint_roundcorners_allow_different_materials";

        public string reflDepthKey => "metallicpaint_refl_depth";

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

        public string refractionTranslucencyWeightKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        //<float name = "metallicpaint_diffuse_image_fade"           val="1."/>
        // <boolean name = "metallicpaint_is_metal"                     val="false"/>
        // <float name = "metallicpaint_transparency"                 val="0.0"/>
        // <float name = "metallicpaint_transparency_image_fade"      val="1."/>
        // <float name = "metallicpaint_cutout_opacity"               val="1.0"/>
        // <boolean name = "metallicpaint_backface_cull"                val="false"/>
        //<float name = "metallicpaint_self_illum_luminance"         val="0.0"/>
        // <float name = "metallicpaint_self_illum_color_temperature" val="0.0"/>
        // <integer name = "metallicpaint_refraction_glossy_samples"    val="1"/>
        // <float name = "noise_Size"                                 val="1.0"/>
        // <string name = "bump_map_asset_name"                        val="Noise"/>
        // <string name = "bump_map_bindings_name"                     val="bump_map_noise_asset"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.isMetal = false;
            material.transparency = 0;
            material.transparencyImageFade = 1;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.refractionGlossySamples = 1;
            material.noiseSize = 1.0f;
            material.bumpMapAssetName = "Noise";
            material.bumpMapBindingsName = "bump_map_noise_asset";
        }
    }
}