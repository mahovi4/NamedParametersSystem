namespace NamedParametersSystemWinControls
{
    partial class BoolParameterControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.cbValue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbValue
            // 
            this.cbValue.AutoSize = true;
            this.cbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbValue.Location = new System.Drawing.Point(0, 0);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(140, 23);
            this.cbValue.TabIndex = 0;
            this.cbValue.Text = "Value";
            this.cbValue.UseVisualStyleBackColor = true;
            this.cbValue.CheckedChanged += new System.EventHandler(this.cbValue_CheckedChanged);
            // 
            // BoolParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbValue);
            this.MinimumSize = new System.Drawing.Size(140, 23);
            this.Name = "BoolParameterControl";
            this.Size = new System.Drawing.Size(140, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip ttMain;
        private CheckBox cbValue;
    }
}