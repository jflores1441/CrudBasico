namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        public Alumno()
        {
            Adjunto = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public int Sexo { get; set; }

        [Required]
        [StringLength(10)]
        public string FechaNacimiento { get; set; }

        public virtual ICollection<Adjunto> Adjunto { get; set; }

        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Alumno> Listar()
        {
            var alumnos = new List<Alumno>();

            try
            {
                using(var ctx=new TestContext())
                {
                    alumnos = ctx.Alumno.ToList();
                }
            }

            catch(Exception e)
            {
                throw;
            }

            return alumnos;
        }

        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();

            try
            {
                using (var ctx = new TestContext())
                {
                    alumno = ctx.Alumno.Include("AlumnoCurso")
                                        .Include("AlumnoCurso.Curso")
                                        .Where(x => x.id == id)
                                        .SingleOrDefault();
                }
            }

            catch (Exception e)
            {
                throw;
            }

            return alumno;
        }


    }
}
