using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class ConcreteSchema : IAssetSchema
    {
        public string colorByObjectKey => "concrete_color_by_object";

        public string diffuseKey => "concrete_color";

        public string diffuseImageFadeKey => "concrete_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "brdf_90_degree_refl";

        public string glossinessKey => "glossiness_asset";

        public string isMetalKey => "concrete_is_metal";

        public string transparencyKey => "concrete_transparency";

        public string transparencyImageFadeKey => "concrete_transparency_image_fade";

        public string refractionIndexKey => "concrete_refraction_index";

        public string refractionTranslucencyWeightKey => "concrete_refraction_translucency_weight";

        public string cutoutOpacityKey => "concrete_cutout_opacity";

        public string backfaceCullKey => "concrete_backface_cull";

        public string selfIllumLuminanceKey => "concrete_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "concrete_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string aoOnKey => "concrete_ao_on";

        public string aoSamplesKey => "concrete_ao_samples";

        public string aoDistanceKey => "concrete_ao_distance";

        public string aoDetailsKey => "concrete_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "concrete_roundcorners_allow_different_materials";

        public string reflDepthKey => "concrete_refl_depth";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

        public string refractionGlossySamplesKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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

        //<float name = "concrete_diffuse_image_fade"             val="1."/>
        //<boolean name = "concrete_is_metal"                       val="false"/>
        //<float name = "concrete_transparency"                   val="0.0"/>
        //<float name = "concrete_transparency_image_fade"        val="0.0"/>
        //<float name = "concrete_refraction_index"               val="1.4"/>
        //<float name = "concrete_refraction_translucency_weight" val=".5"/>
        //<float name = "concrete_cutout_opacity"                 val="1.0"/>
        //<boolean name = "concrete_backface_cull"                  val="false"/>
        //<float name = "concrete_self_illum_luminance"           val="0.0"/>
        //<float name = "concrete_self_illum_color_temperature"   val="0.0"/>
        //<String name = "concrete_glossiness_asset_name"          val="Noise"/>
        //<String name = "concrete_glossiness_asset_bindings"      val="glossiness_asset_noise"/>
        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.isMetal = false;
            material.transparency = 0.0f;
            material.transparencyImageFade = 0.0f;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0.0f;
            material.selfIllumColorTemperature = 0.0f;
            material.glossinessAssetName = "Noise";
            material.glossinessAssetBindings = "glossiness_asset_noise";
        }
    }
}