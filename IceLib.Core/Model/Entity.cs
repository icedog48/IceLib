using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IceLib.Model
{
    /// <summary>
    /// Entidade básica usada para persistência
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Identificador único de um objeto persistido
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Método para identificar se uma instancia foi ou não persistida
        /// </summary>
        /// <returns> True -> Caso a instancia o Id seja igual à zero</returns>
        public virtual bool IsNew() { return this.Id == 0; }

        /// <summary>
        /// Seta o valor da propriedade Id para zero
        /// </summary>
        public virtual void ClearId() 
        {
            this.Id = 0;
        }
    }
}
