using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class MirrorSchema : IAssetSchema
    {
        public string colorByObjectKey => "mirror_color_by_object";

        public string diffuseKey => "mirror_tintcolor";

        public string diffuseImageFadeKey => "mirror_diffuse_image_fade";

        public string reflectivityAt0degKey => "mirror_reflectivity_at_0deg";

        public string reflectivityAt90degKey => "mirror_reflectivity_at_90deg";

        public string glossinessKey => "mirror_glossiness";

        public string isMetalKey => "mirror_is_metal";

        public string transparencyKey => "mirror_transparency";

        public string transparencyImageFadeKey => "mirror_transparency_image_fade";

        public string refractionIndexKey => "mirror_refraction_index";

        public string refractionTranslucencyWeightKey => "mirror_refraction_translucency_weight";

        public string cutoutOpacityKey => "mirror_cutout_opacity";

        public string backfaceCullKey => "mirror_backface_cull";

        public string selfIllumLuminanceKey => "mirror_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "mirror_self_illum_color_temperature";

        public string bumpAmountKey => "mirror_bump_amount";

        public string reflectionGlossySamplesKey => "mirror_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "mirror_refraction_glossy_samples";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "round_corner_radius_map";
        //public string roundcornersRadiusKey => "round_corner_radius_BOF";

        public string roundcornersAllowDifferentMaterialsKey => "mirror_roundcorners_allow_different_materials";

        public string reflDepthKey => "mirror_refl_depth";

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

        public string bumpMapKey
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

        //<float name = "mirror_diffuse_image_fade"             val="1."/>
        //<float name = "mirror_reflectivity_at_0deg"           val=".92"/>
        //<float name = "mirror_reflectivity_at_90deg"          val="1."/>
        //<float name = "mirror_glossiness"                     val="1."/>
        //<boolean name = "mirror_is_metal"                       val="true"/>
        //<float name = "mirror_transparency"                   val="0.0"/>
        //<float name = "mirror_transparency_image_fade"        val="1."/>
        //<float name = "mirror_refraction_index"               val="1.4"/>
        //<float name = "mirror_refraction_translucency_weight" val=".5"/>
        //<float name = "mirror_cutout_opacity"                 val="1.0"/>
        //<boolean name = "mirror_backface_cull"                  val="false"/>
        //<float name = "mirror_self_illum_luminance"           val="0.0"/>
        //<float name = "mirror_self_illum_color_temperature"   val="0.0"/>
        //<float name = "mirror_bump_amount"                    val="0.0"/>
        //<integer name = "mirror_reflection_glossy_samples"      val="8"/>
        //<integer name = "mirror_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt0deg = 0.92f;
            material.reflectivityAt90deg = 1.0f;
            material.glossiness = 1.0f;
            material.isMetal = true;
            material.transparency = 0;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1.4f;
            material.refractionTranslucencyWeight = 0.5f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.bumpAmount = 0.0f;
            material.reflectionGlossySamples = 8;
            material.refractionGlossySamples = 1;
        }
    }
}