namespace Optimizer
{
    partial class OptimizerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOptimize = new System.Windows.Forms.Button();
            this.rtbOutputDetails = new System.Windows.Forms.RichTextBox();
            this.txtIterations = new System.Windows.Forms.TextBox();
            this.rtbSimulationResult = new System.Windows.Forms.RichTextBox();
            this.cbxSurgeMode = new System.Windows.Forms.CheckBox();
            this.lblIterations = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOptimize
            // 
            this.btnOptimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptimize.Location = new System.Drawing.Point(612, 12);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(75, 23);
            this.btnOptimize.TabIndex = 0;
            this.btnOptimize.Text = "Optimize!";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbOutputDetails
            // 
            this.rtbOutputDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutputDetails.Location = new System.Drawing.Point(12, 41);
            this.rtbOutputDetails.Name = "rtbOutputDetails";
            this.rtbOutputDetails.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbOutputDetails.Size = new System.Drawing.Size(675, 134);
            this.rtbOutputDetails.TabIndex = 1;
            this.rtbOutputDetails.Text = "";
            this.rtbOutputDetails.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtIterations
            // 
            this.txtIterations.Location = new System.Drawing.Point(72, 12);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(74, 20);
            this.txtIterations.TabIndex = 2;
            this.txtIterations.Text = "1";
            // 
            // rtbSimulationResult
            // 
            this.rtbSimulationResult.Location = new System.Drawing.Point(13, 181);
            this.rtbSimulationResult.Name = "rtbSimulationResult";
            this.rtbSimulationResult.Size = new System.Drawing.Size(674, 64);
            this.rtbSimulationResult.TabIndex = 3;
            this.rtbSimulationResult.Text = "";
            // 
            // cbxSurgeMode
            // 
            this.cbxSurgeMode.AutoSize = true;
            this.cbxSurgeMode.Location = new System.Drawing.Point(162, 11);
            this.cbxSurgeMode.Name = "cbxSurgeMode";
            this.cbxSurgeMode.Size = new System.Drawing.Size(83, 17);
            this.cbxSurgeMode.TabIndex = 4;
            this.cbxSurgeMode.Text = "Surge mode";
            this.cbxSurgeMode.UseVisualStyleBackColor = true;
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Location = new System.Drawing.Point(13, 12);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(53, 13);
            this.lblIterations.TabIndex = 5;
            this.lblIterations.Text = "Iterations:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 257);
            this.Controls.Add(this.lblIterations);
            this.Controls.Add(this.cbxSurgeMode);
            this.Controls.Add(this.rtbSimulationResult);
            this.Controls.Add(this.txtIterations);
            this.Controls.Add(this.rtbOutputDetails);
            this.Controls.Add(this.btnOptimize);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.RichTextBox rtbOutputDetails;
        private System.Windows.Forms.TextBox txtIterations;
        private System.Windows.Forms.RichTextBox rtbSimulationResult;
        private System.Windows.Forms.CheckBox cbxSurgeMode;
        private System.Windows.Forms.Label lblIterations;
    }
}

