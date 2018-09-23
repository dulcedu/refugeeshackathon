using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DPFP;
using DPFP.Capture;

namespace WebApplication1.Models
{
    public class FingerPrint 
    {
        byte[] Register;
        string Register64;
        UpServicios serv = new UpServicios();

        delegate void Function();	// a simple delegate for marshalling calls from event handlers to the GUI thread
        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate2;

        private DPFP.Processing.Enrollment Enroller;
        private DPFP.Processing.Enrollment Enroller2;
        private DPFP.Template Template;

        private DPFP.Capture.Capture Capturer;
        DPFP.Capture.EventHandler handler;
        public void FingerPrint2018()
        {            
            validacion();
        }
        public void validacion()
        {
            if (ComprobarOperacion().Equals(false))
            {
                //Registro
                Enroller = new DPFP.Processing.Enrollment();                // Create an enrollment.

                OnTemplate2 += this.OnTemplate;
                UpdateStatus();
                ///Se inicia el proceso de cappr
                ///
                Init();
                Start();
            }
            else if (ComprobarOperacion().Equals(true))
            {
                //Verificacion
            }

        }
        private void BtnIniciar_Click(object sender, EventArgs e)
        {

        }
        public Boolean ComprobarOperacion()
        {
            return false;
        }
        private void UpdateStatus()
        {
            // Show number of samples needed.
            SetStatus(String.Format("Fingerprint samples needed: {0}", Enroller.FeaturesNeeded));
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("Can't initiate capture!");
                }
            }
        }

        /// <summary>
        /// Para la captura del biometrico
        /// </summary>
        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    SetPrompt("Can't terminate capture!");
                }
            }
        }

        protected void SetStatus(string status)
        {
            //Se envian los estatus del fingerprint
            //StatusLine.Text = status;
            //}));

            Console.WriteLine(status);
        }


        protected void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = handler;					// Subscribe for capturing events.
                else
                    SetPrompt("Can't initiate capture operation!");
            }
            catch
            {
                Console.WriteLine("Can't initiate capture operation! Error");
            }
        }


        protected void SetPrompt(string prompt)
        {
            //this.Invoke(new Function(delegate ()
            //{
            Console.WriteLine(prompt);
            //Prompt.AppendText(prompt);
            //}));
        }
        protected void MakeReport(string message)
        {
            //this.Invoke(new Function(delegate ()
            //{
            Console.WriteLine(message);
            //StatusText.AppendText(message);
            //}));
        }
        /// <summary>
        /// Se crea metodo para enviar el proceso de detectar el finger print
        /// </summary>
        /// <param name="Sample"></param>
        public void ProcessAsync(DPFP.Sample Sample)
        {
            //Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = serv.ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            // Check quality of the sample and add to enroller if it's good
            if (features != null) try
                {
                    MakeReport("The fingerprint feature set was created.");
                    Enroller.AddFeatures(features);     // Add feature set to template.
                }
                finally
                {
                    UpdateStatus();

                    // Check if template has been created.
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:   // report success and stop capturing

                            OnTemplate(Enroller.Template);
                            //Register64 = Convert.ToBase64String(Enroller.Template.Bytes);
                            SetPrompt("Click Close, and then click Fingerprint Verification.");
                            GuardarRegistroAsync();
                            VerificarRegistro();
                            Stop();
                            //Enroller.Clear();
                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:  // report failure and restart capturing
                            Enroller.Clear();
                            Stop();
                            UpdateStatus();
                            OnTemplate(null);
                            Start();
                            break;
                    }
                }
        }

        public void GuardarRegistroAsync()
        {
            using (FileStream fs = File.Open(@"C:\Registro\Registro.fpt", FileMode.Create, FileAccess.Write))
            {
                Stream templateStream = Template.Serialize(fs);
                //var ipfs = new IpfsClient();

                //const string filename = @"C:\Registro\Registro.fpt";
                //string text = await ipfs.FileSystem.ReadAllTextAsync(filename);
                //Console.WriteLine("HHooaooa "+text);
            }
        }
        public void VerificarRegistro()
        {
            using (FileStream fs = File.OpenRead(@"C:\Registro\Registro.fpt"))
            {
                DPFP.Template template = new DPFP.Template(fs);
                OnTemplate(template);
            }
        }
        //Se crean los metodos para poder leer los eventos del fingerprint

        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            SetPrompt("Scan the same fingerprint again.");
            ProcessAsync(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good.");
            else
                MakeReport("The quality of the fingerprint sample is poor.");
        }

        private void OnTemplate(DPFP.Template template)
        {
            //this.Invoke(new Function(delegate ()
            //{
                Template = template;
                //VerifyButton.Enabled = SaveButton.Enabled = (Template != null);
                if (Template != null)
                    Console.WriteLine("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                else
                    Console.WriteLine("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            //}));
        }

        private void Prompt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
