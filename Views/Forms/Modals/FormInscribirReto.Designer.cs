namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    partial class FormInscribirReto
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelCuerpo = new System.Windows.Forms.Panel();
            this.panelAtletaInfo = new System.Windows.Forms.Panel();
            this.lblAtletaCarrera = new System.Windows.Forms.Label();
            this.lblAtletaNombre = new System.Windows.Forms.Label();
            this.lblInfoAtletaTitulo = new System.Windows.Forms.Label();
            this.cmbReto = new System.Windows.Forms.ComboBox();
            this.lblReto = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnInscribir = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelCuerpo.SuspendLayout();
            this.panelAtletaInfo.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblSubtitulo);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelHeader.Size = new System.Drawing.Size(460, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Location = new System.Drawing.Point(20, 38);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(262, 15);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Asigne una estación o reto del circuito al atleta";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTitulo.Location = new System.Drawing.Point(18, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(232, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Inscripción a Reto del Rally";
            // 
            // panelCuerpo
            // 
            this.panelCuerpo.BackColor = System.Drawing.Color.White;
            this.panelCuerpo.Controls.Add(this.panelAtletaInfo);
            this.panelCuerpo.Controls.Add(this.cmbReto);
            this.panelCuerpo.Controls.Add(this.lblReto);
            this.panelCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCuerpo.Location = new System.Drawing.Point(0, 70);
            this.panelCuerpo.Name = "panelCuerpo";
            this.panelCuerpo.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.panelCuerpo.Size = new System.Drawing.Size(460, 200);
            this.panelCuerpo.TabIndex = 1;
            // 
            // panelAtletaInfo
            // 
            this.panelAtletaInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelAtletaInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAtletaInfo.Controls.Add(this.lblAtletaCarrera);
            this.panelAtletaInfo.Controls.Add(this.lblAtletaNombre);
            this.panelAtletaInfo.Controls.Add(this.lblInfoAtletaTitulo);
            this.panelAtletaInfo.Location = new System.Drawing.Point(25, 15);
            this.panelAtletaInfo.Name = "panelAtletaInfo";
            this.panelAtletaInfo.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.panelAtletaInfo.Size = new System.Drawing.Size(410, 80);
            this.panelAtletaInfo.TabIndex = 0;
            // 
            // lblAtletaCarrera
            // 
            this.lblAtletaCarrera.AutoSize = true;
            this.lblAtletaCarrera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtletaCarrera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAtletaCarrera.Location = new System.Drawing.Point(12, 52);
            this.lblAtletaCarrera.Name = "lblAtletaCarrera";
            this.lblAtletaCarrera.Size = new System.Drawing.Size(127, 15);
            this.lblAtletaCarrera.TabIndex = 2;
            this.lblAtletaCarrera.Text = "Carrera: Ing. Mecatrónica";
            // 
            // lblAtletaNombre
            // 
            this.lblAtletaNombre.AutoSize = true;
            this.lblAtletaNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtletaNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAtletaNombre.Location = new System.Drawing.Point(12, 30);
            this.lblAtletaNombre.Name = "lblAtletaNombre";
            this.lblAtletaNombre.Size = new System.Drawing.Size(185, 19);
            this.lblAtletaNombre.TabIndex = 1;
            this.lblAtletaNombre.Text = "Carlos Eduardo Mendoza";
            // 
            // lblInfoAtletaTitulo
            // 
            this.lblInfoAtletaTitulo.AutoSize = true;
            this.lblInfoAtletaTitulo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoAtletaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInfoAtletaTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblInfoAtletaTitulo.Name = "lblInfoAtletaTitulo";
            this.lblInfoAtletaTitulo.Size = new System.Drawing.Size(130, 13);
            this.lblInfoAtletaTitulo.TabIndex = 0;
            this.lblInfoAtletaTitulo.Text = "ATLETA SELECCIONADO:";
            // 
            // cmbReto
            // 
            this.cmbReto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReto.FormattingEnabled = true;
            this.cmbReto.Location = new System.Drawing.Point(25, 135);
            this.cmbReto.Name = "cmbReto";
            this.cmbReto.Size = new System.Drawing.Size(410, 25);
            this.cmbReto.TabIndex = 2;
            // 
            // lblReto
            // 
            this.lblReto.AutoSize = true;
            this.lblReto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReto.Location = new System.Drawing.Point(25, 115);
            this.lblReto.Name = "lblReto";
            this.lblReto.Size = new System.Drawing.Size(189, 15);
            this.lblReto.TabIndex = 1;
            this.lblReto.Text = "Seleccione la Estación / Reto:";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFooter.Controls.Add(this.btnCancelar);
            this.panelFooter.Controls.Add(this.btnInscribir);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 270);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);
            this.panelFooter.Size = new System.Drawing.Size(460, 60);
            this.panelFooter.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancelar.Location = new System.Drawing.Point(190, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnInscribir
            // 
            this.btnInscribir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.btnInscribir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInscribir.FlatAppearance.BorderSize = 0;
            this.btnInscribir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInscribir.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInscribir.ForeColor = System.Drawing.Color.White;
            this.btnInscribir.Location = new System.Drawing.Point(300, 14);
            this.btnInscribir.Name = "btnInscribir";
            this.btnInscribir.Size = new System.Drawing.Size(135, 34);
            this.btnInscribir.TabIndex = 0;
            this.btnInscribir.Text = "🎯 Inscribir";
            this.btnInscribir.UseVisualStyleBackColor = false;
            this.btnInscribir.Click += new System.EventHandler(this.btnInscribir_Click);
            // 
            // FormInscribirReto
            // 
            this.AcceptButton = this.btnInscribir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(460, 330);
            this.Controls.Add(this.panelCuerpo);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInscribirReto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inscripción al Reto";
            this.Load += new System.EventHandler(this.FormInscribirReto_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelCuerpo.ResumeLayout(false);
            this.panelCuerpo.PerformLayout();
            this.panelAtletaInfo.ResumeLayout(false);
            this.panelAtletaInfo.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCuerpo;
        private System.Windows.Forms.Panel panelAtletaInfo;
        private System.Windows.Forms.Label lblInfoAtletaTitulo;
        private System.Windows.Forms.Label lblAtletaNombre;
        private System.Windows.Forms.Label lblAtletaCarrera;
        private System.Windows.Forms.ComboBox cmbReto;
        private System.Windows.Forms.Label lblReto;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnInscribir;
        private System.Windows.Forms.Button btnCancelar;
    }
}
