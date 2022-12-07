using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class DecalAppearanceSchema : IAssetSchema
    {
        public string colorByObjectKey => "decApp_color_by_object";

        public string diffuseKey => "decApp_diffuse";

        public string diffuseImageFadeKey => "decApp_diffuse_image_fade";

        public string reflectivityAt0degKey => "decApp_reflectivity_at_0deg";

        public string reflectivityAt90degKey => "decApp_reflectivity_at_90deg";

        public string glossinessKey => "decApp_glossiness";

        public string isMetalKey => "decApp_is_metal";

        public string transparencyKey => "decApp_transparency";

        public string transparencyImageFadeKey => "decApp_transparency_image_fade";

        public string refractionIndexKey => "decApp_refraction_index";

        public string refractionTranslucencyWeightKey => "decApp_refraction_translucency_weight";

        public string cutoutOpacityKey => "decApp_cutout_opacity";

        public string backfaceCullKey => "decApp_backface_cull";

        public string selfIllumLuminanceKey => "decApp_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "decApp_self_illum_color_temperature";

        public string selfIllumFilterMapKey => "decApp_self_illum_filter_map";

        public string bumpAmountKey => "decApp_bump_ammount";

        public string bumpMapKey => "decApp_bump_map";

        public string reflectionGlossySamplesKey => "decApp_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "decApp_refraction_glossy_samples";

        public string commonTintToggleKey => "common_Tint_toggle";

        public string commonTintColorKey => "common_Tint_color";

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

        public string reflDepthKey
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

        //<boolean name = "decApp_color_by_object"         val="false"/>
        //<float name = "decApp_diffuse_image_fade"      val="1."/>
        //<float name = "decApp_transparency_image_fade" val="1."/>
        //<boolean name = "decApp_is_metal"                val="false"/>

        public void setDefault(RenderingMaterial material)
        {
            material.colorByObject = false;
            material.diffuseImageFade = 1.0f;
            material.transparencyImageFade = 1.0f;
            material.isMetal = false;
        }
    }
}