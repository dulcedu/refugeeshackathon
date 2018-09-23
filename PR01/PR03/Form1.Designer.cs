namespace PR03
{
    partial class ServicioFingerPrint
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label PromptLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicioFingerPrint));
            this.BtnIniciar = new System.Windows.Forms.Button();
            this.BtnParar = new System.Windows.Forms.Button();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.trTimer = new System.Windows.Forms.Timer(this.components);
            PromptLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PromptLabel
            // 
            resources.ApplyResources(PromptLabel, "PromptLabel");
            PromptLabel.Name = "PromptLabel";
            // 
            // BtnIniciar
            // 
            resources.ApplyResources(this.BtnIniciar, "BtnIniciar");
            this.BtnIniciar.Name = "BtnIniciar";
            this.BtnIniciar.UseVisualStyleBackColor = true;
            this.BtnIniciar.Click += new System.EventHandler(this.BtnIniciar_Click);
            // 
            // BtnParar
            // 
            resources.ApplyResources(this.BtnParar, "BtnParar");
            this.BtnParar.Name = "BtnParar";
            this.BtnParar.UseVisualStyleBackColor = true;
            // 
            // Prompt
            // 
            resources.ApplyResources(this.Prompt, "Prompt");
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.TextChanged += new System.EventHandler(this.Prompt_TextChanged);
            // 
            // trTimer
            // 
            this.trTimer.Enabled = true;
            this.trTimer.Tick += new System.EventHandler(this.trTimer_Tick);
            // 
            // ServicioFingerPrint
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Prompt);
            this.Controls.Add(PromptLabel);
            this.Controls.Add(this.BtnParar);
            this.Controls.Add(this.BtnIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServicioFingerPrint";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnIniciar;
        private System.Windows.Forms.Button BtnParar;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.Timer trTimer;
    }
}

