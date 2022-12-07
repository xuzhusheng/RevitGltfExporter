using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class WallPaintSchema : IAssetSchema
    {
        public string colorByObjectKey => "wallpaint_color_by_object";

        public string diffuseKey => "wallpaint_color";

        public string diffuseImageFadeKey => "wallpaint_diffuse_image_fade";

        public string reflectivityAt0degKey => "Refl_0_deg";

        public string reflectivityAt90degKey => "Refl_90_deg";

        public string glossinessKey => "refl_gloss";

        public string isMetalKey => "wallpaint_is_metal";

        public string transparencyKey => "wallpaint_transparency";

        public string transparencyImageFadeKey => "wallpaint_transparency_image_fade";

        public string refractionIndexKey => "wallpaint_refraction_index";

        public string refractionTranslucencyWeightKey => "wallpaint_refraction_translucency_weight";

        public string cutoutOpacityKey => "wallpaint_cutout_opacity";

        public string backfaceCullKey => "wallpaint_backface_cull";

        public string selfIllumLuminanceKey => "wallpaint_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "wallpaint_self_illum_color_temperature";

        public string bumpAmountKey => "bump_amount";

        public string bumpMapKey => "bump_map_asset";

        public string reflectionGlossySamplesKey => "wallpaint_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "wallpaint_refraction_glossy_samples";

        public string aoOnKey => "wallpaint_ao_on";

        public string aoSamplesKey => "wallpaint_ao_samples";

        public string aoDistanceKey => "wallpaint_ao_distance";

        public string aoDetailsKey => "wallpaint_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "wallpaint_roundcorners_allow_different_materials";

        public string reflDepthKey => "wallpaint_refl_depth";

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

        //<float name = "wallpaint_diffuse_image_fade"             val="1."/>
        //<boolean name = "wallpaint_is_metal"                       val="false"/>
        //<float name = "wallpaint_transparency"                   val="0."/>
        //<float name = "wallpaint_transparency_image_fade"        val="1."/>
        //<float name = "wallpaint_refraction_index"               val="1.4"/>
        //<float name = "wallpaint_refraction_translucency_weight" val=".5"/>
        //<float name = "wallpaint_cutout_opacity"                 val="1."/>
        //<boolean name = "wallpaint_backface_cull"                  val="false"/>
        //<float name = "wallpaint_self_illum_luminance"           val="0"/>
        //<float name = "wallpaint_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "wallpaint_refraction_glossy_samples"      val="1"/>

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