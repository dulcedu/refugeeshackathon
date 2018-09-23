using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR03
{
    class UpServicios
    {
public DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
{
    DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
    DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
    DPFP.FeatureSet features = new DPFP.FeatureSet();
    Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
    if (feedback == DPFP.Capture.CaptureFeedback.Good)
        return features;
    else
        return null;
}
    }
}
