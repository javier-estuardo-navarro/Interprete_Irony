namespace Interprete_Irony
{
    partial class GUI
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
            this.txtSalida = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInterpretar = new System.Windows.Forms.Button();
            this.cargarArchivo = new System.Windows.Forms.Button();
            this.txtEntrada = new System.Windows.Forms.RichTextBox();
            this.btnLimpiarEntrada = new System.Windows.Forms.Button();
            this.btnLimpiarSalida = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSalida
            // 
            this.txtSalida.Location = new System.Drawing.Point(26, 347);
            this.txtSalida.Margin = new System.Windows.Forms.Padding(5);
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.ReadOnly = true;
            this.txtSalida.Size = new System.Drawing.Size(918, 197);
            this.txtSalida.TabIndex = 7;
            this.txtSalida.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 322);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Salida:";
            // 
            // btnInterpretar
            // 
            this.btnInterpretar.Location = new System.Drawing.Point(404, 4);
            this.btnInterpretar.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnInterpretar.Name = "btnInterpretar";
            this.btnInterpretar.Size = new System.Drawing.Size(166, 36);
            this.btnInterpretar.TabIndex = 5;
            this.btnInterpretar.Text = "Interpretar entrada";
            this.btnInterpretar.UseVisualStyleBackColor = true;
            this.btnInterpretar.Click += new System.EventHandler(this.btnInterpretar_Click);
            // 
            // cargarArchivo
            // 
            this.cargarArchivo.Location = new System.Drawing.Point(26, 4);
            this.cargarArchivo.Margin = new System.Windows.Forms.Padding(5);
            this.cargarArchivo.Name = "cargarArchivo";
            this.cargarArchivo.Size = new System.Drawing.Size(258, 36);
            this.cargarArchivo.TabIndex = 8;
            this.cargarArchivo.Text = "Seleccionar archivo de entrada";
            this.cargarArchivo.UseVisualStyleBackColor = true;
            this.cargarArchivo.Click += new System.EventHandler(this.cargarArchivo_Click);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(26, 50);
            this.txtEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(918, 268);
            this.txtEntrada.TabIndex = 9;
            this.txtEntrada.Text = "";
            // 
            // btnLimpiarEntrada
            // 
            this.btnLimpiarEntrada.Location = new System.Drawing.Point(668, 4);
            this.btnLimpiarEntrada.Name = "btnLimpiarEntrada";
            this.btnLimpiarEntrada.Size = new System.Drawing.Size(146, 36);
            this.btnLimpiarEntrada.TabIndex = 10;
            this.btnLimpiarEntrada.Text = "Limpiar entrada";
            this.btnLimpiarEntrada.UseVisualStyleBackColor = true;
            this.btnLimpiarEntrada.Click += new System.EventHandler(this.btnLimpiarEntrada_Click);
            // 
            // btnLimpiarSalida
            // 
            this.btnLimpiarSalida.Location = new System.Drawing.Point(820, 4);
            this.btnLimpiarSalida.Name = "btnLimpiarSalida";
            this.btnLimpiarSalida.Size = new System.Drawing.Size(134, 36);
            this.btnLimpiarSalida.TabIndex = 11;
            this.btnLimpiarSalida.Text = "Limpiar salida";
            this.btnLimpiarSalida.UseVisualStyleBackColor = true;
            this.btnLimpiarSalida.Click += new System.EventHandler(this.btnLimpiarSalida_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 558);
            this.Controls.Add(this.btnLimpiarSalida);
            this.Controls.Add(this.btnLimpiarEntrada);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.cargarArchivo);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInterpretar);
            this.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "GUI";
            this.Text = "Interprete con Irony - Desarrollada por Javier Navarro - Compiladores 2 2S2018";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtSalida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInterpretar;
        private System.Windows.Forms.Button cargarArchivo;
        private System.Windows.Forms.RichTextBox txtEntrada;
        private System.Windows.Forms.Button btnLimpiarEntrada;
        private System.Windows.Forms.Button btnLimpiarSalida;
    }
}

