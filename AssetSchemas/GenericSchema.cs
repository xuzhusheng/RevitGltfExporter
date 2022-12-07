using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    class GenericSchema : IAssetSchema
    {
        public string colorByObjectKey => "color_by_object";

        public string diffuseKey => "generic_diffuse";

        public string diffuseImageFadeKey => "generic_diffuse_image_fade";

        public string reflectivityAt0degKey => "generic_reflectivity_at_0deg";

        public string reflectivityAt90degKey => "generic_reflectivity_at_90deg";

        public string glossinessKey => "generic_glossiness";

        public string isMetalKey => "generic_is_metal";

        public string transparencyKey => "generic_transparency";

        public string transparencyImageFadeKey => "generic_transparency_image_fade";

        public string refractionIndexKey => "generic_refraction_index";

        public string refractionTranslucencyWeightKey => "generic_refraction_translucency_weight";

        public string cutoutOpacityKey => "generic_cutout_opacity";

        public string backfaceCullKey => "generic_backface_cull";

        public string selfIllumLuminanceKey => "generic_self_illum_luminance";

        public string selfIllumColorTemperatureKey => "generic_self_illum_color_temperature";

        public string bumpAmountKey => "generic_bump_amount";

        public string bumpMapKey => "generic_bump_map";

        public string reflectionGlossySamplesKey => "generic_reflection_glossy_samples";

        public string refractionGlossySamplesKey => "generic_refraction_glossy_samples";

        public string aoOnKey => "generic_ao_on";

        public string aoSamplesKey => "generic_ao_samples";

        public string aoDistanceKey => "generic_ao_distance";

        public string aoDetailsKey => "generic_ao_details";

        //重复Key
        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>  
        public string roundcornersRadiusKey => "generic_roundcorners_radius";
        //public string roundcornersRadiusKey => "generic_roundcorners_radius";

        public string roundcornersAllowDifferentMaterialsKey => "generic_roundcorners_allow_different_materials";

        public string reflDepthKey => "generic_refl_depth";

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

        public void setDefault(RenderingMaterial material)
        {

        }
    }
}
