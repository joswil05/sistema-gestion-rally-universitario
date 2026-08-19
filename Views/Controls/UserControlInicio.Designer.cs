namespace SistemadeGestiondeRallyUniversitario
{
    partial class UserControlInicio
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

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardCarreras = new System.Windows.Forms.Panel();
            this.lblTotalCarreras = new System.Windows.Forms.Label();
            this.lblTituloCarreras = new System.Windows.Forms.Label();
            this.cardEstudiantes = new System.Windows.Forms.Panel();
            this.lblTotalEstudiantes = new System.Windows.Forms.Label();
            this.lblTituloEstudiantes = new System.Windows.Forms.Label();
            this.cardRetos = new System.Windows.Forms.Panel();
            this.lblTotalRetos = new System.Windows.Forms.Label();
            this.lblTituloRetos = new System.Windows.Forms.Label();
            this.cardResultados = new System.Windows.Forms.Panel();
            this.lblTotalResultados = new System.Windows.Forms.Label();
            this.lblTituloResultados = new System.Windows.Forms.Label();
            this.panelPodio = new System.Windows.Forms.Panel();
            this.dgvPodio = new System.Windows.Forms.DataGridView();
            this.lblTituloPodio = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.cardCarreras.SuspendLayout();
            this.cardEstudiantes.SuspendLayout();
            this.cardRetos.SuspendLayout();
            this.cardResultados.SuspendLayout();
            this.panelPodio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPodio)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnRefrescar);
            this.panelHeader.Controls.Add(this.lblSubtitulo);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelHeader.Size = new System.Drawing.Size(950, 75);
            this.panelHeader.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefrescar.FlatAppearance.BorderSize = 0;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefrescar.ForeColor = System.Drawing.Color.White;
            this.btnRefrescar.Location = new System.Drawing.Point(810, 18);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(120, 38);
            this.btnRefrescar.TabIndex = 2;
            this.btnRefrescar.Text = "↻ Actualizar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Location = new System.Drawing.Point(20, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(355, 23);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Resumen general del Rally Universitario ULSA";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTitulo.Location = new System.Drawing.Point(18, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(327, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Panel de Control / Inicio";
            // 
            // panelCards
            // 
            this.panelCards.ColumnCount = 4;
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelCards.Controls.Add(this.cardCarreras, 0, 0);
            this.panelCards.Controls.Add(this.cardEstudiantes, 1, 0);
            this.panelCards.Controls.Add(this.cardRetos, 2, 0);
            this.panelCards.Controls.Add(this.cardResultados, 3, 0);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 75);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.RowCount = 1;
            this.panelCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelCards.Size = new System.Drawing.Size(950, 130);
            this.panelCards.TabIndex = 1;
            // 
            // cardCarreras
            // 
            this.cardCarreras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.cardCarreras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCarreras.Controls.Add(this.lblTotalCarreras);
            this.cardCarreras.Controls.Add(this.lblTituloCarreras);
            this.cardCarreras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCarreras.Location = new System.Drawing.Point(18, 18);
            this.cardCarreras.Name = "cardCarreras";
            this.cardCarreras.Padding = new System.Windows.Forms.Padding(10);
            this.cardCarreras.Size = new System.Drawing.Size(224, 94);
            this.cardCarreras.TabIndex = 0;
            // 
            // lblTotalCarreras
            // 
            this.lblTotalCarreras.AutoSize = true;
            this.lblTotalCarreras.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCarreras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.lblTotalCarreras.Location = new System.Drawing.Point(10, 36);
            this.lblTotalCarreras.Name = "lblTotalCarreras";
            this.lblTotalCarreras.Size = new System.Drawing.Size(43, 50);
            this.lblTotalCarreras.TabIndex = 1;
            this.lblTotalCarreras.Text = "0";
            // 
            // lblTituloCarreras
            // 
            this.lblTituloCarreras.AutoSize = true;
            this.lblTituloCarreras.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCarreras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTituloCarreras.Location = new System.Drawing.Point(10, 10);
            this.lblTituloCarreras.Name = "lblTituloCarreras";
            this.lblTituloCarreras.Size = new System.Drawing.Size(174, 23);
            this.lblTituloCarreras.TabIndex = 0;
            this.lblTituloCarreras.Text = "Carreras Registradas";
            // 
            // cardEstudiantes
            // 
            this.cardEstudiantes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.cardEstudiantes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardEstudiantes.Controls.Add(this.lblTotalEstudiantes);
            this.cardEstudiantes.Controls.Add(this.lblTituloEstudiantes);
            this.cardEstudiantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardEstudiantes.Location = new System.Drawing.Point(248, 18);
            this.cardEstudiantes.Name = "cardEstudiantes";
            this.cardEstudiantes.Padding = new System.Windows.Forms.Padding(10);
            this.cardEstudiantes.Size = new System.Drawing.Size(224, 94);
            this.cardEstudiantes.TabIndex = 1;
            // 
            // lblTotalEstudiantes
            // 
            this.lblTotalEstudiantes.AutoSize = true;
            this.lblTotalEstudiantes.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEstudiantes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblTotalEstudiantes.Location = new System.Drawing.Point(10, 36);
            this.lblTotalEstudiantes.Name = "lblTotalEstudiantes";
            this.lblTotalEstudiantes.Size = new System.Drawing.Size(43, 50);
            this.lblTotalEstudiantes.TabIndex = 1;
            this.lblTotalEstudiantes.Text = "0";
            // 
            // lblTituloEstudiantes
            // 
            this.lblTituloEstudiantes.AutoSize = true;
            this.lblTituloEstudiantes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloEstudiantes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblTituloEstudiantes.Location = new System.Drawing.Point(10, 10);
            this.lblTituloEstudiantes.Name = "lblTituloEstudiantes";
            this.lblTituloEstudiantes.Size = new System.Drawing.Size(189, 23);
            this.lblTituloEstudiantes.TabIndex = 0;
            this.lblTituloEstudiantes.Text = "Alumnos Participantes";
            // 
            // cardRetos
            // 
            this.cardRetos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.cardRetos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardRetos.Controls.Add(this.lblTotalRetos);
            this.cardRetos.Controls.Add(this.lblTituloRetos);
            this.cardRetos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRetos.Location = new System.Drawing.Point(478, 18);
            this.cardRetos.Name = "cardRetos";
            this.cardRetos.Padding = new System.Windows.Forms.Padding(10);
            this.cardRetos.Size = new System.Drawing.Size(224, 94);
            this.cardRetos.TabIndex = 2;
            // 
            // lblTotalRetos
            // 
            this.lblTotalRetos.AutoSize = true;
            this.lblTotalRetos.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.lblTotalRetos.Location = new System.Drawing.Point(10, 36);
            this.lblTotalRetos.Name = "lblTotalRetos";
            this.lblTotalRetos.Size = new System.Drawing.Size(43, 50);
            this.lblTotalRetos.TabIndex = 1;
            this.lblTotalRetos.Text = "0";
            // 
            // lblTituloRetos
            // 
            this.lblTituloRetos.AutoSize = true;
            this.lblTituloRetos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblTituloRetos.Location = new System.Drawing.Point(10, 10);
            this.lblTituloRetos.Name = "lblTituloRetos";
            this.lblTituloRetos.Size = new System.Drawing.Size(117, 23);
            this.lblTituloRetos.TabIndex = 0;
            this.lblTituloRetos.Text = "Retos Activos";
            // 
            // cardResultados
            // 
            this.cardResultados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.cardResultados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardResultados.Controls.Add(this.lblTotalResultados);
            this.cardResultados.Controls.Add(this.lblTituloResultados);
            this.cardResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardResultados.Location = new System.Drawing.Point(708, 18);
            this.cardResultados.Name = "cardResultados";
            this.cardResultados.Padding = new System.Windows.Forms.Padding(10);
            this.cardResultados.Size = new System.Drawing.Size(224, 94);
            this.cardResultados.TabIndex = 3;
            // 
            // lblTotalResultados
            // 
            this.lblTotalResultados.AutoSize = true;
            this.lblTotalResultados.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTotalResultados.Location = new System.Drawing.Point(10, 36);
            this.lblTotalResultados.Name = "lblTotalResultados";
            this.lblTotalResultados.Size = new System.Drawing.Size(43, 50);
            this.lblTotalResultados.TabIndex = 1;
            this.lblTotalResultados.Text = "0";
            // 
            // lblTituloResultados
            // 
            this.lblTituloResultados.AutoSize = true;
            this.lblTituloResultados.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTituloResultados.Location = new System.Drawing.Point(10, 10);
            this.lblTituloResultados.Name = "lblTituloResultados";
            this.lblTituloResultados.Size = new System.Drawing.Size(160, 23);
            this.lblTituloResultados.TabIndex = 0;
            this.lblTituloResultados.Text = "Tiempos Marcados";
            // 
            // panelPodio
            // 
            this.panelPodio.BackColor = System.Drawing.Color.White;
            this.panelPodio.Controls.Add(this.dgvPodio);
            this.panelPodio.Controls.Add(this.lblTituloPodio);
            this.panelPodio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPodio.Location = new System.Drawing.Point(0, 205);
            this.panelPodio.Name = "panelPodio";
            this.panelPodio.Padding = new System.Windows.Forms.Padding(20);
            this.panelPodio.Size = new System.Drawing.Size(950, 395);
            this.panelPodio.TabIndex = 2;
            // 
            // dgvPodio
            // 
            this.dgvPodio.AllowUserToAddRows = false;
            this.dgvPodio.AllowUserToDeleteRows = false;
            this.dgvPodio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPodio.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvPodio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPodio.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPodio.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPodio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPodio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPodio.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPodio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPodio.EnableHeadersVisualStyles = false;
            this.dgvPodio.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvPodio.Location = new System.Drawing.Point(20, 62);
            this.dgvPodio.MultiSelect = false;
            this.dgvPodio.Name = "dgvPodio";
            this.dgvPodio.ReadOnly = true;
            this.dgvPodio.RowHeadersVisible = false;
            this.dgvPodio.RowHeadersWidth = 51;
            this.dgvPodio.RowTemplate.Height = 35;
            this.dgvPodio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPodio.Size = new System.Drawing.Size(910, 313);
            this.dgvPodio.TabIndex = 1;
            // 
            // lblTituloPodio
            // 
            this.lblTituloPodio.AutoSize = true;
            this.lblTituloPodio.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloPodio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPodio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTituloPodio.Location = new System.Drawing.Point(20, 20);
            this.lblTituloPodio.Name = "lblTituloPodio";
            this.lblTituloPodio.Padding = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.lblTituloPodio.Size = new System.Drawing.Size(387, 42);
            this.lblTituloPodio.TabIndex = 0;
            this.lblTituloPodio.Text = "🏆 Podio y Clasificación General Actual";
            // 
            // UserControlInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.panelPodio);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserControlInicio";
            this.Size = new System.Drawing.Size(950, 600);
            this.Load += new System.EventHandler(this.UserControlInicio_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.cardCarreras.ResumeLayout(false);
            this.cardCarreras.PerformLayout();
            this.cardEstudiantes.ResumeLayout(false);
            this.cardEstudiantes.PerformLayout();
            this.cardRetos.ResumeLayout(false);
            this.cardRetos.PerformLayout();
            this.cardResultados.ResumeLayout(false);
            this.cardResultados.PerformLayout();
            this.panelPodio.ResumeLayout(false);
            this.panelPodio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPodio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.TableLayoutPanel panelCards;
        private System.Windows.Forms.Panel cardCarreras;
        private System.Windows.Forms.Label lblTotalCarreras;
        private System.Windows.Forms.Label lblTituloCarreras;
        private System.Windows.Forms.Panel cardEstudiantes;
        private System.Windows.Forms.Label lblTotalEstudiantes;
        private System.Windows.Forms.Label lblTituloEstudiantes;
        private System.Windows.Forms.Panel cardRetos;
        private System.Windows.Forms.Label lblTotalRetos;
        private System.Windows.Forms.Label lblTituloRetos;
        private System.Windows.Forms.Panel cardResultados;
        private System.Windows.Forms.Label lblTotalResultados;
        private System.Windows.Forms.Label lblTituloResultados;
        private System.Windows.Forms.Panel panelPodio;
        private System.Windows.Forms.Label lblTituloPodio;
        private System.Windows.Forms.DataGridView dgvPodio;
    }
}
