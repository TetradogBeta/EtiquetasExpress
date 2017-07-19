/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 07/19/2017
 * Hora: 20:52
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Etiquetas_Express
{
	/// <summary>
	/// Interaction logic for Etiqueta.xaml
	/// </summary>
	public partial class Etiqueta : UserControl
	{
		public Etiqueta()
		{
			InitializeComponent();
		}
		public Etiqueta GenerarCopia()
		{
			RowDefinition row;
			ColumnDefinition column;
			Etiqueta etiqueta=new Etiqueta();
			
			etiqueta.gEtiqueta.Height=gEtiqueta.Height;
			etiqueta.gEtiqueta.Width=gEtiqueta.Width;
			
			etiqueta.gEtiqueta.ColumnDefinitions.Clear();
			for(int i=0;i<gEtiqueta.ColumnDefinitions.Count;i++){
				column=new ColumnDefinition();
				column.Width=gEtiqueta.ColumnDefinitions[i].Width;
				etiqueta.gEtiqueta.ColumnDefinitions.Add(column);
			}
			
			etiqueta.gEtiqueta.RowDefinitions.Clear();
			for(int i=0;i<gEtiqueta.RowDefinitions.Count;i++){
				row=new RowDefinition();
				row.Height=gEtiqueta.RowDefinitions[i].Height;
				etiqueta.gEtiqueta.RowDefinitions.Add(row);
			}
			etiqueta.Background=Background;
			etiqueta.txtBody.Background=txtBody.Background;
			return etiqueta;
		}
		public void PonerPlantilla(Etiqueta etiqueta)
		{
			RowDefinition row;
			ColumnDefinition column;
			
			gEtiqueta.MinHeight=etiqueta.gEtiqueta.MinHeight;
			gEtiqueta.MaxHeight=etiqueta.gEtiqueta.MaxHeight;
			
			gEtiqueta.MinWidth=etiqueta.gEtiqueta.MinWidth;
			gEtiqueta.MaxWidth=etiqueta.gEtiqueta.MaxWidth;
			
			gEtiqueta.ColumnDefinitions.Clear();
			for(int i=0;i<etiqueta.gEtiqueta.ColumnDefinitions.Count;i++)
			{
				column=new ColumnDefinition();
				column.Width=etiqueta.gEtiqueta.ColumnDefinitions[i].Width;
				gEtiqueta.ColumnDefinitions.Add(column);
			}
			
			gEtiqueta.RowDefinitions.Clear();
			for(int i=0;i<etiqueta.gEtiqueta.RowDefinitions.Count;i++)
				{
				row=new RowDefinition();
				row.Height=etiqueta.gEtiqueta.RowDefinitions[i].Height;
				gEtiqueta.RowDefinitions.Add(row);
			}
				
			
			Background=etiqueta.Background;
			txtBody.Background=etiqueta.txtBody.Background;
			
		}
	
	}
}