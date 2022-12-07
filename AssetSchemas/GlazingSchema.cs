using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class GlazingSchema : IAssetSchema
    {
        public string colorByObjectKey => "glazing_color_by_object";

        //Key重复
        //<map_Bindings source = "transmittance_map"                      destination="generic_diffuse"/>
        //<map_operator source = "refr_color"                             destination="generic_diffuse"/>   
        public string diffuseKey => "transmittance_map";
        //public string diffuseKey => "refr_color";

        public string diffuseImageFadeKey => "glazing_diffuse_image_fade";

        public string reflectivityAt0degKey => "brdf_0_degree_refl";

        public string reflectivityAt90degKey => "glazing_reflectivity_at_90deg";

        public string glossinessKey => "glazing_glossiness";

        public string isMetalKey => "glazing_is_metal";

        public string transparencyKey => "glazing_transparency";

        public string transparencyImageFadeKey => "glazing_transparency_image_fade";

        public string refractionIndexKey => "glazing_refraction_index";

        public string refractionTranslucencyWeightKey => "glazing_refraction_translucency_weight";

        public string cutoutOpacityKey => "glazing_cutout_opacity";

        public string backfaceCullKey => "glazing_backface_cull";

        public string selfIllumLuminanceKey => "glazing_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "glazing_self_illum_color_temperature";

        public string reflectionGlossySamplesKey => "glazing_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "glazing_refraction_glossy_samples";

        public string reflDepthKey => "glazing_refl_depth";

        public string refrDepthKey => "glazing_refr_depth";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

        public string selfIllumFilterMapKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string bumpAmountKey
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

        //<float name = "glazing_diffuse_image_fade"             val="1."/>
        //<float name = "glazing_reflectivity_at_90deg"          val="1.0"/>
        //<float name = "glazing_glossiness"                     val="1.0"/>
        //<boolean name = "glazing_is_metal"                       val="false"/>
        //<float name = "glazing_transparency"                   val="0.5"/>
        //<float name = "glazing_transparency_image_fade"        val="1."/>
        //<float name = "glazing_refraction_index"               val="1."/>
        //<float name = "glazing_refraction_translucency_weight" val="0.0"/>
        //<float name = "glazing_cutout_opacity"                 val="1.0"/>
        //<boolean name = "glazing_backface_cull"                  val="false"/>
        //<float name = "glazing_self_illum_luminance"           val="0.0"/>
        //<float name = "glazing_self_illum_color_temperature"   val="0.0"/>
        //<integer name = "glazing_reflection_glossy_samples"      val="1"/>
        //<integer name = "glazing_refraction_glossy_samples"      val="1"/>

        public void setDefault(RenderingMaterial material)
        {
            material.diffuseImageFade = 1;
            material.reflectivityAt90deg = 1.0f;
            material.glossiness = 1.0f;
            material.isMetal = false;
            material.transparency = 0.5f;
            material.transparencyImageFade = 1;
            material.refractionIndex = 1;
            material.refractionTranslucencyWeight = 0.0f;
            material.cutoutOpacity = 1.0f;
            material.backfaceCull = false;
            material.selfIllumLuminance = 0;
            material.selfIllumColorTemperature = 0.0f;
            material.reflectionGlossySamples = 1;
            material.refractionGlossySamples = 1;
        }
    }
}