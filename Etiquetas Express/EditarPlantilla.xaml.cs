/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/07/2017
 * Hora: 2:26
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
	/// Interaction logic for EditarPlantilla.xaml
	/// </summary>
	public partial class EditarPlantilla : Window
	{
		Etiqueta plantilla;
		public EditarPlantilla()
		{
			InitializeComponent();
		}

		public Etiqueta Plantilla {
			get {
				return plantilla;
			}
			set {
				plantilla = value;
			}
		}
	}
}