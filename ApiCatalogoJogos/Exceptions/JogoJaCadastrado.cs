using System;


namespace ApiCatalogoJogos.Exceptions
{

        public class JogoJaCadastrado : Exception
        {
            public JogoJaCadastrado()
                : base("Este já jogo está cadastrado")
            { }
        }
    
}
