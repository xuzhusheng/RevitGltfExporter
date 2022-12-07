using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class HardwoodSchema : IAssetSchema
    {
        public string colorByObjectKey => "hardwood_color_by_object";

        public string diffuseKey => "hardwood_color";

        public string diffuseImageFadeKey => "hardwood_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "brdf_90_degree_refl";

        public string glossinessKey => "glossiness";

        public string isMetalKey => "hardwood_is_metal";

        public string transparencyKey => "hardwood_transparency";

        public string transparencyImageFadeKey => "hardwood_transparency_image_fade";

        public string refractionIndexKey => "hardwood_refraction_index";

        public string refractionTranslucencyWeightKey => "hardwood_refraction_translucency_weight";

        public string cutoutOpacityKey => "hardwood_cutout_opacity";

        public string backfaceCullKey => "hardwood_backface_cull";

        public string selfIllumLuminanceKey => "hardwood_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "hardwood_self_illum_color_temperature";

        public string bumpAmountKey => "hardwood_imperfections_amount";

        public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "hardwood_refraction_glossy_samples";

        public string aoOnKey => "hardwood_ao_on";

        public string aoSamplesKey => "hardwood_ao_samples";

        public string aoDistanceKey => "hardwood_ao_distance";

        public string aoDetailsKey => "hardwood_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "hardwood_roundcorners_allow_different_materials";

        public string reflDepthKey => "hardwood_refl_depth";

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

        //<boolean name = "hardwood_color_by_object"                val="false"/>
        //<float name = "hardwood_diffuse_image_fade"             val="1."/>
        //<boolean name = "hardwood_is_metal"                       val="false"/>
        //<float name = "hardwood_transparency"                   val="0.0"/>
        //<float name = "hardwood_transparency_image_fade"        val="1."/>
        //<float name = "hardwood_refraction_index"               val="1.4"/>
        //<float name = "hardwood_refraction_translucency_weight" val=".5"/>
        //<float name = "hardwood_cutout_opacity"                 val="1.0"/>
        //<boolean name = "hardwood_backface_cull"                  val="false"/>
        //<float name = "hardwood_self_illum_luminance"           val="0.0"/>
        //<float name = "hardwood_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "hardwood_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.colorByObject = false;
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