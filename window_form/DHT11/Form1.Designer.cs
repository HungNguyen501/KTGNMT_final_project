namespace DHT11
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.send_data_button = new System.Windows.Forms.Button();
            this.status_lable = new System.Windows.Forms.Label();
            this.status_value_lable = new System.Windows.Forms.Label();
            this.temp_lable = new System.Windows.Forms.Label();
            this.temp_value_lable = new System.Windows.Forms.Label();
            this.humid_value_lable = new System.Windows.Forms.Label();
            this.humid_lable = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.zedGraphControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zedGraphControl.Location = new System.Drawing.Point(432, 17);
            this.zedGraphControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 5D;
            this.zedGraphControl.ScrollMaxY2 = 5D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 1D;
            this.zedGraphControl.ScrollMinY2 = 3D;
            this.zedGraphControl.SelectButtons = System.Windows.Forms.MouseButtons.Right;
            this.zedGraphControl.Size = new System.Drawing.Size(1243, 421);
            this.zedGraphControl.TabIndex = 17;
            this.zedGraphControl.UseExtendedPrintDialog = true;
            this.zedGraphControl.Load += new System.EventHandler(this.zedGraphControl_Load);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.zedGraphControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zedGraphControl1.Location = new System.Drawing.Point(432, 464);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 5D;
            this.zedGraphControl1.ScrollMaxY2 = 5D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 1D;
            this.zedGraphControl1.ScrollMinY2 = 3D;
            this.zedGraphControl1.SelectButtons = System.Windows.Forms.MouseButtons.Right;
            this.zedGraphControl1.Size = new System.Drawing.Size(1243, 416);
            this.zedGraphControl1.TabIndex = 22;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Controls.Add(this.send_data_button);
            this.groupBox3.Controls.Add(this.status_lable);
            this.groupBox3.Controls.Add(this.status_value_lable);
            this.groupBox3.Controls.Add(this.temp_lable);
            this.groupBox3.Controls.Add(this.temp_value_lable);
            this.groupBox3.Controls.Add(this.humid_value_lable);
            this.groupBox3.Controls.Add(this.humid_lable);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 17);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(387, 519);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            // 
            // send_data_button
            // 
            this.send_data_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.send_data_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_data_button.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.send_data_button.Location = new System.Drawing.Point(98, 327);
            this.send_data_button.Name = "send_data_button";
            this.send_data_button.Size = new System.Drawing.Size(145, 51);
            this.send_data_button.TabIndex = 7;
            this.send_data_button.Text = "Send Data";
            this.send_data_button.UseVisualStyleBackColor = false;
            this.send_data_button.Click += new System.EventHandler(this.sendDataButton_Click);
            // 
            // status_lable
            // 
            this.status_lable.AutoSize = true;
            this.status_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_lable.Location = new System.Drawing.Point(27, 191);
            this.status_lable.Name = "status_lable";
            this.status_lable.Size = new System.Drawing.Size(74, 25);
            this.status_lable.TabIndex = 6;
            this.status_lable.Text = "Status:";
            this.status_lable.Click += new System.EventHandler(this.status_lable_Click);
            // 
            // status_value_lable
            // 
            this.status_value_lable.AutoSize = true;
            this.status_value_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status_value_lable.ForeColor = System.Drawing.Color.YellowGreen;
            this.status_value_lable.Location = new System.Drawing.Point(224, 191);
            this.status_value_lable.Name = "status_value_lable";
            this.status_value_lable.Size = new System.Drawing.Size(80, 25);
            this.status_value_lable.TabIndex = 5;
            this.status_value_lable.Text = "Normal";
            this.status_value_lable.Click += new System.EventHandler(this.status_value_lable_Click);
            // 
            // temp_lable
            // 
            this.temp_lable.AutoSize = true;
            this.temp_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temp_lable.Location = new System.Drawing.Point(27, 124);
            this.temp_lable.Name = "temp_lable";
            this.temp_lable.Size = new System.Drawing.Size(172, 25);
            this.temp_lable.TabIndex = 4;
            this.temp_lable.Text = "Temperature (°C):";
            this.temp_lable.Click += new System.EventHandler(this.temp_lable_Click);
            // 
            // temp_value_lable
            // 
            this.temp_value_lable.AutoSize = true;
            this.temp_value_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temp_value_lable.Location = new System.Drawing.Point(224, 124);
            this.temp_value_lable.Name = "temp_value_lable";
            this.temp_value_lable.Size = new System.Drawing.Size(34, 25);
            this.temp_value_lable.TabIndex = 3;
            this.temp_value_lable.Text = "25";
            this.temp_value_lable.Click += new System.EventHandler(this.temp_value_lable_Click);
            // 
            // humid_value_lable
            // 
            this.humid_value_lable.AutoSize = true;
            this.humid_value_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humid_value_lable.Location = new System.Drawing.Point(224, 55);
            this.humid_value_lable.Name = "humid_value_lable";
            this.humid_value_lable.Size = new System.Drawing.Size(34, 25);
            this.humid_value_lable.TabIndex = 2;
            this.humid_value_lable.Text = "40";
            this.humid_value_lable.Click += new System.EventHandler(this.humid_value_lable_Click);
            // 
            // humid_lable
            // 
            this.humid_lable.AutoSize = true;
            this.humid_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humid_lable.Location = new System.Drawing.Point(27, 55);
            this.humid_lable.Name = "humid_lable";
            this.humid_lable.Size = new System.Drawing.Size(130, 25);
            this.humid_lable.TabIndex = 0;
            this.humid_lable.Text = "Humidity (%):";
            this.humid_lable.Click += new System.EventHandler(this.humid_lable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1700, 894);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.zedGraphControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ZedGraph.ZedGraphControl zedGraphControl;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label temp_lable;
        private System.Windows.Forms.Label temp_value_lable;
        private System.Windows.Forms.Label humid_value_lable;
        private System.Windows.Forms.Label humid_lable;
        private System.Windows.Forms.Label status_lable;
        private System.Windows.Forms.Label status_value_lable;
        private System.Windows.Forms.Button send_data_button;
    }
}

