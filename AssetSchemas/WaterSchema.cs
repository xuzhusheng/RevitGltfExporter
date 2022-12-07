using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class WaterSchema : IAssetSchema
    {
        public string colorByObjectKey => "water_color_by_object";

        public string diffuseKey => "diffuse_color";

        public string diffuseImageFadeKey => "water_diffuse_image_fade";

        public string reflectivityAt0degKey => "water_reflectivity_at_0deg";

        public string reflectivityAt90degKey => "water_reflectivity_at_90deg";

        public string glossinessKey => "water_glossiness";

        public string isMetalKey => "water_is_metal";

        public string transparencyKey => "transparency";

        public string transparencyImageFadeKey => "water_transparency_image_fade";

        public string refractionIndexKey => "water_refraction_index";

        public string refractionTranslucencyWeightKey => "water_refraction_translucency_weight";

        public string cutoutOpacityKey => "water_cutout_opacity";

        public string backfaceCullKey => "water_backface_cull";

        public string selfIllumLuminanceKey => "water_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "water_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "refraction_glossy_samples";

        public string reflDepthKey => "water_refl_depth";

        public string refrDepthKey => "water_refr_depth";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

        public string selfIllumFilterMapKey
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

        //<float name = "water_diffuse_image_fade"             val="1."/>
        //<float name = "water_reflectivity_at_0deg"           val=".1"/>
        //<float name = "water_reflectivity_at_90deg"          val=".5"/>
        //<float name = "water_glossiness"                     val="1."/>
        //<boolean name = "water_is_metal"                       val="false"/>
        //<float name = "water_transparency"                   val=".6"/>
        //<float name = "water_transparency_image_fade"        val="1."/>
        //<float name = "water_refraction_index"               val="1.33"/>
        //<float name = "water_refraction_translucency_weight" val=".5"/> 
        //<float name = "water_cutout_opacity"                 val="1."/>
        //<boolean name = "water_backface_cull"                  val="false"/>
        //<float name = "water_self_illum_luminance"           val="0"/>
        //<float name = "water_self_illum_color_temperature"   val="0.0"/>
        //<String name = "water_bump_map_asset_name"            val="Noise"/>
        //<String name = "water_bump_map_asset_bindings"        val="bump_map_asset_noise"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt0deg = 0.1f;
            material.reflectivityAt90deg = 0.5f;
            material.glossiness = 1.0f;
            material.isMetal = false;
            material.transparency = 0.6f ;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1.33f;
            material.refractionTranslucencyWeight = 0.5f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.bumpMapAssetName = "Noise";
            material.bumpMapAssetBindings = "bump_map_asset_noise";
        }
    }
}