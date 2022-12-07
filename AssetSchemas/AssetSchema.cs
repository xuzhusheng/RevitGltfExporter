using System;
using System.Collections.Generic;
using System.Text;

namespace RevitGltfExporter
{
    public interface IAssetSchema
    {
        string colorByObjectKey { get; }

        string diffuseKey { get; }

        string diffuseImageFadeKey { get; }

        string reflectivityAt0degKey { get; }

        string reflectivityAt90degKey { get; }

        string glossinessKey { get; }

        string isMetalKey { get; }

        string transparencyKey { get; }

        string transparencyImageFadeKey { get; }

        string refractionIndexKey { get; }

        string refractionTranslucencyWeightKey { get; }

        string cutoutOpacityKey { get; }

        string backfaceCullKey { get; }

        string selfIllumLuminanceKey { get; }

        string selfIllumColorTemperatureKey { get; }

        string selfIllumFilterMapKey { get; }

        string bumpAmountKey { get; }

        string bumpMapKey { get; }

        string reflectionGlossySamplesKey { get; }

        string refractionGlossySamplesKey { get; }

        string aoOnKey { get; }

        string aoSamplesKey { get; }

        string aoDistanceKey { get; }

        string aoDetailsKey { get; }

        //<map_bindings source = "round_corner_radius_map"                destination="generic_roundcorners_radius"/>
        //<map_operator source = "round_corner_radius_BOF"                destination="generic_roundcorners_radius"/>
        string roundcornersRadiusKey { get; }

        string roundcornersAllowDifferentMaterialsKey { get; }

        string reflDepthKey { get; }

        string refrDepthKey { get; }

        string commonTintToggleKey { get; }

        string commonTintColorKey { get; }

        void setDefault(RenderingMaterial material);
    }
}
