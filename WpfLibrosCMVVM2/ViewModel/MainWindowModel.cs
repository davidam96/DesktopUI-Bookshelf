using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfLibrosCMVVM2.Conexiones;
using WpfLibrosCMVVM2.Model;

namespace WpfLibrosCMVVM2.ViewModel
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        //OBJETO CONEXION
        private Conexion conexion;


        //PROPIEDAD "ARRAYLIST" CON LA LISTA DE LIBROS
        private ObservableCollection<CLibro> listaDeLibros;
        public ObservableCollection<CLibro> ListaDeLibros
        {
            get { return listaDeLibros; }
            set
            {
                listaDeLibros = value;
                OnPropertyChanged("ListaDeLibros");
            }
        }


        //PROPIEDADES TEXTO DE TEXTBOXES

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                OnPropertyChanged("Titulo");
            }
        }

        private string isbn;
        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged("ISBN");
            }
        }

        private string autor;
        public string Autor
        {
            get { return autor; }
            set
            {
                autor = value;
                OnPropertyChanged("Autor");
            }
        }

        private string editorial;
        public string Editorial
        {
            get { return editorial; }
            set
            {
                editorial = value;
                OnPropertyChanged("Editorial");
            }
        }


        //PROPIEDADES ACTIVAR BOTONES Y TEXTBOXES

        private bool activarTextBoxes;
        public bool ActivarTextBoxes
        {
            get { return activarTextBoxes; }
            set
            {
                activarTextBoxes = value;
                OnPropertyChanged("ActivarTextBoxes");
            }
        }

        private bool activarBtnNuevo;
        public bool ActivarBtnNuevo
        {
            get { return activarBtnNuevo; }
            set
            {
                activarBtnNuevo = value;
                OnPropertyChanged("ActivarBtnNuevo");
            }
        }

        private bool activarBtnModificar;
        public bool ActivarBtnModificar
        {
            get { return activarBtnModificar; }
            set
            {
                activarBtnModificar = value;
                OnPropertyChanged("ActivarBtnModificar");
            }
        }

        private bool activarBtnEliminar;
        public bool ActivarBtnEliminar
        {
            get { return activarBtnEliminar; }
            set
            {
                activarBtnEliminar = value;
                OnPropertyChanged("ActivarBtnEliminar");
            }
        }

        private bool activarBtnGuardar;
        public bool ActivarBtnGuardar
        {
            get { return activarBtnGuardar; }
            set
            {
                activarBtnGuardar = value;
                OnPropertyChanged("ActivarBtnGuardar");
            }
        }

        private bool activarBtnActualizar;
        public bool ActivarBtnActualizar
        {
            get { return activarBtnActualizar; }
            set
            {
                activarBtnActualizar = value;
                OnPropertyChanged("ActivarBtnActualizar");
            }
        }

        private bool activarBtnCancelar;
        public bool ActivarBtnCancelar
        {
            get { return activarBtnCancelar; }
            set
            {
                activarBtnCancelar = value;
                OnPropertyChanged("ActivarBtnCancelar");
            }
        }


        //PROPIEDAD LIBRO SELECCIONADO
        private CLibro libroSeleccionado;
        public CLibro LibroSeleccionado
        {
            get { return libroSeleccionado; }
            set
            {
                libroSeleccionado = value;

                //Rellenamos los TextBoxes con los datos
                //del libro seleccionado, siempre y cuando
                //dicho libro seleccionado no sea nulo.
                if (LibroSeleccionado != null)
                {
                    Titulo = LibroSeleccionado.Titulo;
                    ISBN = LibroSeleccionado.ISBN;
                    Autor = LibroSeleccionado.Autor;
                    Editorial = LibroSeleccionado.Editorial;
                }

                //Si hemos seleccionado un item del ListBox,
                //activamos los botones Modificar y Eliminar.
                ActivarBtnModificar = true;
                ActivarBtnEliminar = true;

                OnPropertyChanged("LibroSeleccionado");
            }
        }


        //PROPIEDADES ICOMMAND (BOTONES)
        public ICommand ComandoNuevo { get; set; }
        public ICommand ComandoModificar { get; set; }
        public ICommand ComandoEliminar { get; set; }
        public ICommand ComandoGuardar { get; set; }
        public ICommand ComandoActualizar { get; set; }
        public ICommand ComandoCancelar { get; set; }


        //CONSTRUCTOR
        public MainWindowModel()
        {
            conexion = new Conexion();

            ListaDeLibros = conexion.ObtenerLibros();

            ComandoNuevo = new Command(AccionNuevo);
            ComandoModificar = new Command(AccionModificar);
            ComandoEliminar = new Command(AccionEliminar);
            ComandoGuardar = new Command(AccionGuardar);
            ComandoActualizar = new Command(AccionActualizar);
            ComandoCancelar = new Command(AccionCancelar);

            initControles();
        }


        //METODOS SOBRE FUNCIONALIDADES VARIAS

        private void initControles()
        {
            disableAllControles();
            ActivarBtnNuevo = true;
            if (LibroSeleccionado != null)
            {
                ActivarBtnModificar = true;
                ActivarBtnEliminar = true;
            }
        }

        private void disableAllControles()
        {
            ActivarBtnNuevo = false;
            ActivarBtnModificar = false;
            ActivarBtnEliminar = false;
            ActivarBtnGuardar = false;
            ActivarBtnActualizar = false;
            ActivarBtnCancelar = false;

            ActivarTextBoxes = false;
        }

        private void EmptyTextBoxes()
        {
            Titulo = "";
            ISBN = "";
            Autor = "";
            Editorial = "";
        }


        //METODOS ASOCIADOS A COMANDOS (DE LOS BOTONES)

        private void AccionNuevo(object obj)
        {
            //Vacío el texto de las TextBoxes.
            EmptyTextBoxes();

            //Desactivo todos los controles de la interfaz.
            disableAllControles();

            //Activo solo los controles que me interesan.
            ActivarTextBoxes = true;
            ActivarBtnGuardar = true;
            ActivarBtnCancelar = true;
        }

        private void AccionModificar(object obj)
        {
            //Desactivo todos los controles de la interfaz.
            disableAllControles();

            //Activo solo los controles que me interesan.
            ActivarTextBoxes = true;
            ActivarBtnActualizar = true;
            ActivarBtnCancelar = true;
        }

        private void AccionEliminar(object obj)
        {
            //Preguntamos al usuario si realmente quiere 
            //eliminar el registro
            MessageBoxResult respuesta;
            respuesta = MessageBox.Show("Está seguro de que desea eliminar el libro \""+Titulo+"\" ?",
                                    "Eliminar libro", MessageBoxButton.YesNo);
            
            if (respuesta == MessageBoxResult.Yes)
            {
                //Borramos el libro seleccionado de la BDD
                conexion.BorrarLibro(LibroSeleccionado.ISBN);

                //Borramos el libro de la ListaDeLibros
                ListaDeLibros.Remove(LibroSeleccionado);

                //Borramos los rastros de texto del libro en los TextBoxes
                EmptyTextBoxes();
            }     

            //Reinicio la interfaz
            initControles();
        }

        private void AccionGuardar(object obj)
        {
            //Creo un nuevo objeto libro con los
            //datos que hay en los TextBoxes.
            CLibro NuevoLibro = new CLibro();
            NuevoLibro.Titulo = Titulo;
            NuevoLibro.ISBN = ISBN;
            NuevoLibro.Autor = Autor;
            NuevoLibro.Editorial = Editorial;

            //Intento insertar el nuevo libro en la BDD
            Exception exception = null;
            try 
            { 
                conexion.GuardarNuevoLibro(NuevoLibro); 
            }
            catch (Exception ex) 
            {
                //Capturo la excepción para más adelante.
                exception = ex;
                //Si ha habido error, vacío las TextBoxes.
                EmptyTextBoxes();
            }

            //Añado el nuevo libro a ListaDeLibros, si previamente
            //no ha habido error en el insertaje en BDD.
            if (exception == null)
                ListaDeLibros.Add(NuevoLibro);

            //Reinicio la interfaz.
            initControles();

            //Vuelvo al libro en el que estaba
            LibroSeleccionado = LibroSeleccionado;

        }

        private void AccionActualizar(object obj)
        {
            //TEN EN CUENTA QUE EL BOTON ACTUALIZAR SOLO
            //SE UTILIZA PARA CONFIRMAR UNA MODIFICACION,
            //NO SE UTILIZA CON NINGUNA OTRA ACCIÓN!!!

            //Creo un nuevo objeto libro con los
            //datos que hay en los TextBoxes.
            CLibro LibroModificado = new CLibro();
            LibroModificado.Titulo = Titulo;
            LibroModificado.ISBN = ISBN;
            LibroModificado.Autor = Autor;
            LibroModificado.Editorial = Editorial;

            //Actualizo el libro modificado en la BDD
            conexion.ActualizarLibroExistente(LibroModificado, LibroSeleccionado.ISBN);

            //Actualizo el libro modificado en ListaDeLibros
            ListaDeLibros[ListaDeLibros.IndexOf(LibroSeleccionado)] = LibroModificado;

            //Reinicio la interfaz
            initControles();

            //Vuelvo al libro en el que estaba
            LibroSeleccionado = LibroModificado;
        }

        private void AccionCancelar(object obj)
        {
            //Si hemos seleccionado un libro previamente, cuando
            //cancele la accion quiero que me lo vuelva a mostrar.
            if (LibroSeleccionado != null)
            LibroSeleccionado = LibroSeleccionado;

            //Reinicio la interfaz
            initControles();
        }


        //METODOS OBLIGATORIOS DE LA INTERFAZ 'INotifyPropertyChanged'
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
