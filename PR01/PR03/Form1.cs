using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPFP;
using DPFP.Capture;
using Ipfs.Api;
using Nethereum.KeyStore;
//using Nethereum.XUnitEthereumClients;
//using Xunit;

namespace PR03
{
    public partial class ServicioFingerPrint : Form, DPFP.Capture.EventHandler
    {
        byte[] Register;
        string Register64;
        sqlfunciones sqlin = new sqlfunciones();
        UpServicios serv = new UpServicios();

        delegate void Function();	// a simple delegate for marshalling calls from event handlers to the GUI thread
        public delegate void OnTemplateEventHandler(DPFP.Template template);

        public event OnTemplateEventHandler OnTemplate2;

        private DPFP.Processing.Enrollment Enroller;
        private DPFP.Processing.Enrollment Enroller2;
        private DPFP.Template Template;

        private DPFP.Capture.Capture Capturer;

        public ServicioFingerPrint()
        {
            InitializeComponent();


            //Assert.Equal(ecKey.GetPrivateKey(), key.ToHex(true));
        }
        public void validacion()
        {
            if(ComprobarOperacion().Equals("NO"))
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
            else if (ComprobarOperacion().Equals("SI"))
            {
                //Verificacion            
                VerificarRegistro();
            }

        }
        private void BtnIniciar_Click(object sender, EventArgs e)
        {

        }
        public string ComprobarOperacion()
        {
            var SJISJ = sqlin.RetValUserTipo();
            return SJISJ;
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
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                    SetPrompt("Can't initiate capture operation!");
            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public async Task ProcessAsync(DPFP.Sample Sample)
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
                            Stop();
                            CrearCuenta();
                            trTimer.Enabled = true;
                            sqlin.HabilitarBiometricoNO();
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

        public void CrearCuenta()
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var keyStorePbkdf2Service = new KeyStorePbkdf2Service();
            var password = "testPassword";
            var json = keyStorePbkdf2Service.EncryptAndGenerateKeyStoreAsJson(password, ecKey.GetPrivateKeyAsBytes(), ecKey.GetPublicAddress());
            var key = keyStorePbkdf2Service.DecryptKeyStoreFromJson(password, json);
        }

        public async void ShouldTransferEther()
        {
            //var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            //var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BA1";
            //var receipt = await web3.Eth.GetEtherTransferService()
            //    .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m);

            //var balance = await web3.Eth.GetBalance.SendRequestAsync(toAddress);
            //Assert.Equal(1.11m, Web3.Web3.Convert.FromWei(balance));
        }

        public async Task GuardarRegistroAsync()
        {
            using (FileStream fs = File.Open(@"C:\Registro\Registro.fpt", FileMode.Create, FileAccess.Write))
            {
                Stream templateStream = Template.Serialize(fs);
                var ipfs = new IpfsClient();

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
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                //VerifyButton.Enabled = SaveButton.Enabled = (Template != null);
                if (Template != null)
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                else
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            }));
        }

        private void Prompt_TextChanged(object sender, EventArgs e)
        {

        }

        private void trTimer_Tick(object sender, EventArgs e)
        {
            if(sqlin.RetValUser() != "NO" && sqlin.RetValUser() != "0")
            {
                trTimer.Enabled = false;
            validacion();
                
            }
        }
    }
}
