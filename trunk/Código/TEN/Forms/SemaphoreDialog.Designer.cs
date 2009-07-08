namespace TEN.Forms
{
	partial class SemaphoreDialog
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
			this.btnOk = new System.Windows.Forms.Button();
			this.lblDescription = new System.Windows.Forms.Label();
			this.cycleInterval = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.greenTime = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.listSemaphores = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(14, 142);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(11, 13);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(71, 13);
			this.lblDescription.TabIndex = 1;
			this.lblDescription.Text = "Cycle Interval";
			// 
			// cycleInterval
			// 
			this.cycleInterval.Location = new System.Drawing.Point(93, 10);
			this.cycleInterval.MaxLength = 2;
			this.cycleInterval.Name = "cycleInterval";
			this.cycleInterval.Size = new System.Drawing.Size(50, 20);
			this.cycleInterval.TabIndex = 2;
			this.cycleInterval.Text = "5";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(145, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Green time";
			// 
			// greenTime
			// 
			this.greenTime.Location = new System.Drawing.Point(148, 46);
			this.greenTime.MaxLength = 4;
			this.greenTime.Name = "greenTime";
			this.greenTime.Size = new System.Drawing.Size(50, 20);
			this.greenTime.TabIndex = 4;
			this.greenTime.Text = "10";
			// 
			// btnCancel
			// 
			this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(95, 142);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(146, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "seconds";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.listSemaphores);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.greenTime);
			this.groupBox1.Location = new System.Drawing.Point(14, 36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 100);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Traffic Lights Timing";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(204, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "seconds";
			// 
			// listSemaphores
			// 
			this.listSemaphores.FormattingEnabled = true;
			this.listSemaphores.Location = new System.Drawing.Point(9, 19);
			this.listSemaphores.Name = "listSemaphores";
			this.listSemaphores.ScrollAlwaysVisible = true;
			this.listSemaphores.Size = new System.Drawing.Size(120, 69);
			this.listSemaphores.TabIndex = 5;
			this.listSemaphores.SelectedIndexChanged += new System.EventHandler(this.listSemaphores_SelectedIndexChanged);
			// 
			// SemaphoreDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(292, 180);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cycleInterval);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SemaphoreDialog";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Semaphore Temporization";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SemaphoreDialog_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox cycleInterval;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox greenTime;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listSemaphores;
		private System.Windows.Forms.Label label3;
	}
}