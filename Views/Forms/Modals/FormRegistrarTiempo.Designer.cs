namespace SistemadeGestiondeRallyUniversitario.Views.Forms.Modals
{
    partial class FormRegistrarTiempo
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
            this.panelResumenOficial = new System.Windows.Forms.Panel();
            this.lblTiempoOficialCalculado = new System.Windows.Forms.Label();
            this.cmbPenalizaciones = new System.Windows.Forms.ComboBox();
            this.lblPenalizaciones = new System.Windows.Forms.Label();
            this.panelChecklist = new System.Windows.Forms.Panel();
            this.chkPenalizacionFalta = new System.Windows.Forms.CheckBox();
            this.chkEstacion5 = new System.Windows.Forms.CheckBox();
            this.chkEstacion4 = new System.Windows.Forms.CheckBox();
            this.chkEstacion3 = new System.Windows.Forms.CheckBox();
            this.chkEstacion2 = new System.Windows.Forms.CheckBox();
            this.chkEstacion1 = new System.Windows.Forms.CheckBox();
            this.lblTituloChecklist = new System.Windows.Forms.Label();
            this.numSegundos = new System.Windows.Forms.NumericUpDown();
            this.lblSegundos = new System.Windows.Forms.Label();
            this.numMinutos = new System.Windows.Forms.NumericUpDown();
            this.lblMinutos = new System.Windows.Forms.Label();
            this.lblTiempoManual = new System.Windows.Forms.Label();
            this.panelCronoDigital = new System.Windows.Forms.Panel();
            this.btnReiniciarCrono = new System.Windows.Forms.Button();
            this.btnLlegadaMeta = new System.Windows.Forms.Button();
            this.btnDarSalida = new System.Windows.Forms.Button();
            this.lblDisplayCrono = new System.Windows.Forms.Label();
            this.cmbCarrera = new System.Windows.Forms.ComboBox();
            this.lblCarrera = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelCuerpo.SuspendLayout();
            this.panelResumenOficial.SuspendLayout();
            this.panelChecklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSegundos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutos)).BeginInit();
            this.panelCronoDigital.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(520, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Location = new System.Drawing.Point(20, 38);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(355, 15);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Control en caliente de salida, meta, checklist de circuito y penalizaciones";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTitulo.Location = new System.Drawing.Point(18, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(315, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Mesa de Cronometraje y Salida/Meta";
            // 
            // panelCuerpo
            // 
            this.panelCuerpo.AutoScroll = true;
            this.panelCuerpo.BackColor = System.Drawing.Color.White;
            this.panelCuerpo.Controls.Add(this.panelResumenOficial);
            this.panelCuerpo.Controls.Add(this.cmbPenalizaciones);
            this.panelCuerpo.Controls.Add(this.lblPenalizaciones);
            this.panelCuerpo.Controls.Add(this.panelChecklist);
            this.panelCuerpo.Controls.Add(this.lblTituloChecklist);
            this.panelCuerpo.Controls.Add(this.numSegundos);
            this.panelCuerpo.Controls.Add(this.lblSegundos);
            this.panelCuerpo.Controls.Add(this.numMinutos);
            this.panelCuerpo.Controls.Add(this.lblMinutos);
            this.panelCuerpo.Controls.Add(this.lblTiempoManual);
            this.panelCuerpo.Controls.Add(this.panelCronoDigital);
            this.panelCuerpo.Controls.Add(this.cmbCarrera);
            this.panelCuerpo.Controls.Add(this.lblCarrera);
            this.panelCuerpo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCuerpo.Location = new System.Drawing.Point(0, 70);
            this.panelCuerpo.Name = "panelCuerpo";
            this.panelCuerpo.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.panelCuerpo.Size = new System.Drawing.Size(520, 480);
            this.panelCuerpo.TabIndex = 1;
            // 
            // panelResumenOficial
            // 
            this.panelResumenOficial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelResumenOficial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.panelResumenOficial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResumenOficial.Controls.Add(this.lblTiempoOficialCalculado);
            this.panelResumenOficial.Location = new System.Drawing.Point(25, 420);
            this.panelResumenOficial.Name = "panelResumenOficial";
            this.panelResumenOficial.Padding = new System.Windows.Forms.Padding(6);
            this.panelResumenOficial.Size = new System.Drawing.Size(470, 42);
            this.panelResumenOficial.TabIndex = 12;
            // 
            // lblTiempoOficialCalculado
            // 
            this.lblTiempoOficialCalculado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTiempoOficialCalculado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempoOficialCalculado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTiempoOficialCalculado.Location = new System.Drawing.Point(6, 6);
            this.lblTiempoOficialCalculado.Name = "lblTiempoOficialCalculado";
            this.lblTiempoOficialCalculado.Size = new System.Drawing.Size(456, 28);
            this.lblTiempoOficialCalculado.TabIndex = 0;
            this.lblTiempoOficialCalculado.Text = "⏱ Total Oficial: 00:00.00 (0.00 seg)";
            this.lblTiempoOficialCalculado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPenalizaciones
            // 
            this.cmbPenalizaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPenalizaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPenalizaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPenalizaciones.FormattingEnabled = true;
            this.cmbPenalizaciones.Location = new System.Drawing.Point(25, 385);
            this.cmbPenalizaciones.Name = "cmbPenalizaciones";
            this.cmbPenalizaciones.Size = new System.Drawing.Size(470, 23);
            this.cmbPenalizaciones.TabIndex = 11;
            this.cmbPenalizaciones.SelectedIndexChanged += new System.EventHandler(this.CmbPenalizaciones_SelectedIndexChanged);
            // 
            // lblPenalizaciones
            // 
            this.lblPenalizaciones.AutoSize = true;
            this.lblPenalizaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPenalizaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPenalizaciones.Location = new System.Drawing.Point(25, 365);
            this.lblPenalizaciones.Name = "lblPenalizaciones";
            this.lblPenalizaciones.Size = new System.Drawing.Size(199, 15);
            this.lblPenalizaciones.TabIndex = 10;
            this.lblPenalizaciones.Text = "Penalización Extraordinaria / Juez:";
            // 
            // panelChecklist
            // 
            this.panelChecklist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChecklist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelChecklist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChecklist.Controls.Add(this.chkPenalizacionFalta);
            this.panelChecklist.Controls.Add(this.chkEstacion5);
            this.panelChecklist.Controls.Add(this.chkEstacion4);
            this.panelChecklist.Controls.Add(this.chkEstacion3);
            this.panelChecklist.Controls.Add(this.chkEstacion2);
            this.panelChecklist.Controls.Add(this.chkEstacion1);
            this.panelChecklist.Location = new System.Drawing.Point(25, 230);
            this.panelChecklist.Name = "panelChecklist";
            this.panelChecklist.Padding = new System.Windows.Forms.Padding(10);
            this.panelChecklist.Size = new System.Drawing.Size(470, 125);
            this.panelChecklist.TabIndex = 9;
            // 
            // chkPenalizacionFalta
            // 
            this.chkPenalizacionFalta.AutoSize = true;
            this.chkPenalizacionFalta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPenalizacionFalta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.chkPenalizacionFalta.Location = new System.Drawing.Point(235, 75);
            this.chkPenalizacionFalta.Name = "chkPenalizacionFalta";
            this.chkPenalizacionFalta.Size = new System.Drawing.Size(206, 19);
            this.chkPenalizacionFalta.TabIndex = 5;
            this.chkPenalizacionFalta.Text = "⚠️ Falta técnica en pista (+15s)";
            this.chkPenalizacionFalta.UseVisualStyleBackColor = true;
            this.chkPenalizacionFalta.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // chkEstacion5
            // 
            this.chkEstacion5.AutoSize = true;
            this.chkEstacion5.Checked = true;
            this.chkEstacion5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstacion5.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstacion5.Location = new System.Drawing.Point(235, 45);
            this.chkEstacion5.Name = "chkEstacion5";
            this.chkEstacion5.Size = new System.Drawing.Size(183, 19);
            this.chkEstacion5.TabIndex = 4;
            this.chkEstacion5.Text = "✓ Estación 5: Sprint y Tirolina";
            this.chkEstacion5.UseVisualStyleBackColor = true;
            this.chkEstacion5.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // chkEstacion4
            // 
            this.chkEstacion4.AutoSize = true;
            this.chkEstacion4.Checked = true;
            this.chkEstacion4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstacion4.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstacion4.Location = new System.Drawing.Point(235, 15);
            this.chkEstacion4.Name = "chkEstacion4";
            this.chkEstacion4.Size = new System.Drawing.Size(188, 19);
            this.chkEstacion4.TabIndex = 3;
            this.chkEstacion4.Text = "✓ Estación 4: Puente de Fango";
            this.chkEstacion4.UseVisualStyleBackColor = true;
            this.chkEstacion4.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // chkEstacion3
            // 
            this.chkEstacion3.AutoSize = true;
            this.chkEstacion3.Checked = true;
            this.chkEstacion3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstacion3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstacion3.Location = new System.Drawing.Point(15, 75);
            this.chkEstacion3.Name = "chkEstacion3";
            this.chkEstacion3.Size = new System.Drawing.Size(193, 19);
            this.chkEstacion3.TabIndex = 2;
            this.chkEstacion3.Text = "✓ Estación 3: Pared de Escalada";
            this.chkEstacion3.UseVisualStyleBackColor = true;
            this.chkEstacion3.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // chkEstacion2
            // 
            this.chkEstacion2.AutoSize = true;
            this.chkEstacion2.Checked = true;
            this.chkEstacion2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstacion2.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstacion2.Location = new System.Drawing.Point(15, 45);
            this.chkEstacion2.Name = "chkEstacion2";
            this.chkEstacion2.Size = new System.Drawing.Size(196, 19);
            this.chkEstacion2.TabIndex = 1;
            this.chkEstacion2.Text = "✓ Estación 2: Traslado de Carga";
            this.chkEstacion2.UseVisualStyleBackColor = true;
            this.chkEstacion2.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // chkEstacion1
            // 
            this.chkEstacion1.AutoSize = true;
            this.chkEstacion1.Checked = true;
            this.chkEstacion1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstacion1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstacion1.Location = new System.Drawing.Point(15, 15);
            this.chkEstacion1.Name = "chkEstacion1";
            this.chkEstacion1.Size = new System.Drawing.Size(198, 19);
            this.chkEstacion1.TabIndex = 0;
            this.chkEstacion1.Text = "✓ Estación 1: Pista de Obstáculos";
            this.chkEstacion1.UseVisualStyleBackColor = true;
            this.chkEstacion1.CheckedChanged += new System.EventHandler(this.Checklist_CheckedChanged);
            // 
            // lblTituloChecklist
            // 
            this.lblTituloChecklist.AutoSize = true;
            this.lblTituloChecklist.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloChecklist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTituloChecklist.Location = new System.Drawing.Point(25, 210);
            this.lblTituloChecklist.Name = "lblTituloChecklist";
            this.lblTituloChecklist.Size = new System.Drawing.Size(209, 15);
            this.lblTituloChecklist.TabIndex = 8;
            this.lblTituloChecklist.Text = "📋 Checklist de Estaciones Superadas:";
            // 
            // numSegundos
            // 
            this.numSegundos.DecimalPlaces = 2;
            this.numSegundos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSegundos.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numSegundos.Location = new System.Drawing.Point(270, 175);
            this.numSegundos.Maximum = new decimal(new int[] {
            5999,
            0,
            0,
            131072});
            this.numSegundos.Name = "numSegundos";
            this.numSegundos.Size = new System.Drawing.Size(225, 26);
            this.numSegundos.TabIndex = 7;
            this.numSegundos.ValueChanged += new System.EventHandler(this.NumTiempo_ValueChanged);
            // 
            // lblSegundos
            // 
            this.lblSegundos.AutoSize = true;
            this.lblSegundos.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSegundos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSegundos.Location = new System.Drawing.Point(270, 158);
            this.lblSegundos.Name = "lblSegundos";
            this.lblSegundos.Size = new System.Drawing.Size(61, 13);
            this.lblSegundos.TabIndex = 6;
            this.lblSegundos.Text = "Seg (0-59):";
            // 
            // numMinutos
            // 
            this.numMinutos.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMinutos.Location = new System.Drawing.Point(25, 175);
            this.numMinutos.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numMinutos.Name = "numMinutos";
            this.numMinutos.Size = new System.Drawing.Size(225, 26);
            this.numMinutos.TabIndex = 5;
            this.numMinutos.ValueChanged += new System.EventHandler(this.NumTiempo_ValueChanged);
            // 
            // lblMinutos
            // 
            this.lblMinutos.AutoSize = true;
            this.lblMinutos.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinutos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMinutos.Location = new System.Drawing.Point(25, 158);
            this.lblMinutos.Name = "lblMinutos";
            this.lblMinutos.Size = new System.Drawing.Size(30, 13);
            this.lblMinutos.TabIndex = 4;
            this.lblMinutos.Text = "Min:";
            // 
            // lblTiempoManual
            // 
            this.lblTiempoManual.AutoSize = true;
            this.lblTiempoManual.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempoManual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTiempoManual.Location = new System.Drawing.Point(25, 138);
            this.lblTiempoManual.Name = "lblTiempoManual";
            this.lblTiempoManual.Size = new System.Drawing.Size(155, 15);
            this.lblTiempoManual.TabIndex = 3;
            this.lblTiempoManual.Text = "Tiempo Bruto de Circuito:";
            // 
            // panelCronoDigital
            // 
            this.panelCronoDigital.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCronoDigital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelCronoDigital.Controls.Add(this.btnReiniciarCrono);
            this.panelCronoDigital.Controls.Add(this.btnLlegadaMeta);
            this.panelCronoDigital.Controls.Add(this.btnDarSalida);
            this.panelCronoDigital.Controls.Add(this.lblDisplayCrono);
            this.panelCronoDigital.Location = new System.Drawing.Point(25, 45);
            this.panelCronoDigital.Name = "panelCronoDigital";
            this.panelCronoDigital.Padding = new System.Windows.Forms.Padding(10);
            this.panelCronoDigital.Size = new System.Drawing.Size(470, 85);
            this.panelCronoDigital.TabIndex = 2;
            // 
            // btnReiniciarCrono
            // 
            this.btnReiniciarCrono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReiniciarCrono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnReiniciarCrono.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReiniciarCrono.FlatAppearance.BorderSize = 0;
            this.btnReiniciarCrono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReiniciarCrono.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReiniciarCrono.ForeColor = System.Drawing.Color.White;
            this.btnReiniciarCrono.Location = new System.Drawing.Point(415, 45);
            this.btnReiniciarCrono.Name = "btnReiniciarCrono";
            this.btnReiniciarCrono.Size = new System.Drawing.Size(45, 32);
            this.btnReiniciarCrono.TabIndex = 3;
            this.btnReiniciarCrono.Text = "↺";
            this.btnReiniciarCrono.UseVisualStyleBackColor = false;
            this.btnReiniciarCrono.Click += new System.EventHandler(this.BtnReiniciarCrono_Click);
            // 
            // btnLlegadaMeta
            // 
            this.btnLlegadaMeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLlegadaMeta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLlegadaMeta.FlatAppearance.BorderSize = 0;
            this.btnLlegadaMeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLlegadaMeta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLlegadaMeta.ForeColor = System.Drawing.Color.White;
            this.btnLlegadaMeta.Location = new System.Drawing.Point(215, 45);
            this.btnLlegadaMeta.Name = "btnLlegadaMeta";
            this.btnLlegadaMeta.Size = new System.Drawing.Size(185, 32);
            this.btnLlegadaMeta.TabIndex = 2;
            this.btnLlegadaMeta.Text = "🏁 MARCAR META";
            this.btnLlegadaMeta.UseVisualStyleBackColor = false;
            this.btnLlegadaMeta.Click += new System.EventHandler(this.BtnLlegadaMeta_Click);
            // 
            // btnDarSalida
            // 
            this.btnDarSalida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.btnDarSalida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDarSalida.FlatAppearance.BorderSize = 0;
            this.btnDarSalida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDarSalida.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDarSalida.ForeColor = System.Drawing.Color.White;
            this.btnDarSalida.Location = new System.Drawing.Point(10, 45);
            this.btnDarSalida.Name = "btnDarSalida";
            this.btnDarSalida.Size = new System.Drawing.Size(185, 32);
            this.btnDarSalida.TabIndex = 1;
            this.btnDarSalida.Text = "▶ DAR SALIDA";
            this.btnDarSalida.UseVisualStyleBackColor = false;
            this.btnDarSalida.Click += new System.EventHandler(this.BtnDarSalida_Click);
            // 
            // lblDisplayCrono
            // 
            this.lblDisplayCrono.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisplayCrono.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayCrono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblDisplayCrono.Location = new System.Drawing.Point(10, 10);
            this.lblDisplayCrono.Name = "lblDisplayCrono";
            this.lblDisplayCrono.Size = new System.Drawing.Size(450, 32);
            this.lblDisplayCrono.TabIndex = 0;
            this.lblDisplayCrono.Text = "00:00.00";
            this.lblDisplayCrono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCarrera
            // 
            this.cmbCarrera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCarrera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarrera.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCarrera.FormattingEnabled = true;
            this.cmbCarrera.Location = new System.Drawing.Point(25, 12);
            this.cmbCarrera.Name = "cmbCarrera";
            this.cmbCarrera.Size = new System.Drawing.Size(470, 25);
            this.cmbCarrera.TabIndex = 1;
            // 
            // lblCarrera
            // 
            this.lblCarrera.AutoSize = true;
            this.lblCarrera.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarrera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCarrera.Location = new System.Drawing.Point(25, -5);
            this.lblCarrera.Name = "lblCarrera";
            this.lblCarrera.Size = new System.Drawing.Size(183, 15);
            this.lblCarrera.TabIndex = 0;
            this.lblCarrera.Text = "Carrera / Delegación en Pista:";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFooter.Controls.Add(this.btnCancelar);
            this.panelFooter.Controls.Add(this.btnGuardar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 550);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);
            this.panelFooter.Size = new System.Drawing.Size(520, 60);
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
            this.btnCancelar.Location = new System.Drawing.Point(230, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(340, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(155, 34);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "💾 Registrar Tiempo";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // FormRegistrarTiempo
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(520, 610);
            this.Controls.Add(this.panelCuerpo);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegistrarTiempo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mesa de Salida y Meta - Cronometraje";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRegistrarTiempo_FormClosing);
            this.Load += new System.EventHandler(this.FormRegistrarTiempo_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelCuerpo.ResumeLayout(false);
            this.panelCuerpo.PerformLayout();
            this.panelResumenOficial.ResumeLayout(false);
            this.panelChecklist.ResumeLayout(false);
            this.panelChecklist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSegundos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutos)).EndInit();
            this.panelCronoDigital.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelCuerpo;
        private System.Windows.Forms.ComboBox cmbCarrera;
        private System.Windows.Forms.Label lblCarrera;
        private System.Windows.Forms.Panel panelCronoDigital;
        private System.Windows.Forms.Label lblDisplayCrono;
        private System.Windows.Forms.Button btnDarSalida;
        private System.Windows.Forms.Button btnLlegadaMeta;
        private System.Windows.Forms.Button btnReiniciarCrono;
        private System.Windows.Forms.Label lblTiempoManual;
        private System.Windows.Forms.Label lblMinutos;
        private System.Windows.Forms.NumericUpDown numMinutos;
        private System.Windows.Forms.Label lblSegundos;
        private System.Windows.Forms.NumericUpDown numSegundos;
        private System.Windows.Forms.Label lblTituloChecklist;
        private System.Windows.Forms.Panel panelChecklist;
        private System.Windows.Forms.CheckBox chkEstacion1;
        private System.Windows.Forms.CheckBox chkEstacion2;
        private System.Windows.Forms.CheckBox chkEstacion3;
        private System.Windows.Forms.CheckBox chkEstacion4;
        private System.Windows.Forms.CheckBox chkEstacion5;
        private System.Windows.Forms.CheckBox chkPenalizacionFalta;
        private System.Windows.Forms.Label lblPenalizaciones;
        private System.Windows.Forms.ComboBox cmbPenalizaciones;
        private System.Windows.Forms.Panel panelResumenOficial;
        private System.Windows.Forms.Label lblTiempoOficialCalculado;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
