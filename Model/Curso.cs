namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        public Curso()
        {
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        /// <summary>
        /// Método que enlistará todos los cursos de un alumno
        /// </summary>
        /// <param name="Alumno_id"></param>
        /// <returns></returns>
        public List<Curso> Todos(int Alumno_id = 0) //Pendiente mas adelante si el alumno ha tomado un curso, vamos a hacer que lo ignore, para eso haremos un inner join
        {
            var cursos = new List<Curso>();

            try
            {
                using (var ctx = new TestContext())//Abrimos el contexto
                {
                    if (Alumno_id > 0)
                    {    
                        //Query de SQL***************************
                        //select* from Curso c
                        //where id NOT IN(Select Curso_id FROM AlumnoCurso ac WHERE ac.Curso_id= c.id AND ac.Alumno_id= 3009)

                        //Forma Sencilla (Usando Query de SQL) con la referencia de System.Data.Client
                        //cursos = ctx.Database.SqlQuery<Curso>("select c.* from Curso c where id NOT IN(Select Curso_id FROM AlumnoCurso ac WHERE ac.Curso_id= c.id AND ac.Alumno_id= @Alumno_id)", new SqlParameter("Alumno_id", Alumno_id)).ToList();

                        //Devuelve un listado de enteros con los id de los cursos que ha tomado el alumno
                        var cursos_tomados = ctx.AlumnoCurso.Where(x => x.Alumno_id == Alumno_id)
                                                            .Select(x => x.Curso_id)
                                                            .ToList();


                        //Indicamos que trabaje con la tabla Curso y que enliste todos los registros, para usar ToList() hay que agregar la referencia System.Linq
                        //condicion- obtener los cursos donde no coincidan los id de la lista cursos_tomados
                        cursos = ctx.Curso
                            .Where(x => !cursos_tomados.Contains(x.id))
                            .ToList();
                    }

                    else
                    {
                        //Indicamos que trabaje con la tabla Curso y que enliste todos los registros de la tabla
                        cursos = ctx.Curso.ToList(); 
                    }

                }
            }

            catch (Exception e)
            {
                throw;
            }

            return cursos;
        }



    }
}
