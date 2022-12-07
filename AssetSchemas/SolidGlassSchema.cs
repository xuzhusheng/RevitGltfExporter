using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class SolidGlassSchema : IAssetSchema
    {
        public string colorByObjectKey => "solidglass_color_by_object";

        //重复Key
        //<map_interface source = "solidglass_transmittance_custom_color"     destination="generic_diffuse"/>
        //<map_operator source = "diffuse_color"                             destination="generic_diffuse"/>
        public string diffuseKey => "solidglass_transmittance_custom_color";
        //public string diffuseKey => "diffuse_color";

        public string diffuseImageFadeKey => "solidglass_diffuse_image_fade";

        public string reflectivityAt0degKey => "solidglass_reflectance";

        public string reflectivityAt90degKey => "solidglass_reflectance";

        public string glossinessKey => "solidglass_glossiness";

        public string isMetalKey => "solidglass_is_metal";

        public string transparencyKey => "solidglass_transparency";

        public string transparencyImageFadeKey => "solidglass_transparency_image_fade";

        public string refractionIndexKey => "solidglass_refr_ior";

        public string refractionTranslucencyWeightKey => "solidglass_refraction_translucency_weight";

        public string cutoutOpacityKey => "solidglass_cutout_opacity";

        public string backfaceCullKey => "solidglass_backface_cull";

        public string selfIllumLuminanceKey => "solidglass_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "solidglass_self_illum_color_temperature";

        public string bumpAmountKey => "solidglass_bump_amount";

        //重复Key
        //<map_interface source = "solidglass_bump_map"                       destination="generic_bump_map"/>
        //<map_bindings source = "bump_map_asset"                            destination="generic_bump_map"/>
        public string bumpMapKey => "solidglass_bump_map";
        //public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "reflection_glossy_samples";

        public string refractionGlossySamplesKey => "refraction_glossy_samples";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "solidglass_roundcorners_allow_different_materials";

        public string reflDepthKey => "solidglass_refl_depth";

        public string refrDepthKey => "solidglass_refr_depth";

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

        //<color name = "solidglass_diffuse"                        valR="0.0" valG="0.0" valB="0.0" valA="1."/>
        //<float name = "solidglass_diffuse_image_fade"             val="1."/>
        //<boolean name = "solidglass_is_metal"                       val="false"/>
        //<float name = "solidglass_transparency"                   val="0.85"/>
        //<float name = "solidglass_transparency_image_fade"        val="1."/>
        //<float name = "solidglass_refraction_translucency_weight" val="0.5"/>
        //<float name = "solidglass_cutout_opacity"                 val="1."/>
        //<boolean name = "solidglass_backface_cull"                  val="false"/>
        //<float name = "solidglass_self_illum_luminance"           val="0.0"/>
        //<float name = "solidglass_self_illum_color_temperature"   val="0.0"/>
        //<String name = "solidglass_bump_map_asset_name"            val="Noise"/>
        //<String name = "solidglass_bump_map_asset_bindings"        val="bump_map_asset_noise"/>

        public void setDefault(RenderingMaterial material)
        {
            //material.diffuse = Color.FromArgb(1, 0, 0, 0);
            material.diffuseColor = new double[] { 0, 0, 0 };
            material.diffuseImageFade = 1;
            material.isMetal = false;
            material.transparency = 0.85f ;
            material.transparencyImageFade = 1;
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