import axios from "axios"
import { useState, useEffect } from "react"

import banner from '../assets/img/banner_cadastro.svg'

import '../assets/css/Cadastro.css'

export default function Cadastro() {
    const [listatipos, setListaTipos] = useState([])
    const [nome, setNome] = useState('')
    const [senha, setSenha] = useState('')
    const [StatusConta, setStatusConta] = useState(true)
    const [email, setEmail] = useState('')
    const [idTipo, setIdTipo] = useState('')

    const listar_tipo = () => {
        axios('http://localhost:5000/api/Tipo', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                console.log(resposta)
                setListaTipos(resposta.data)
            }
        }).catch(erro => console.log(erro))
    }

    const Cadastrar = (event) => {
        event.preventDefault();
        let Usuario = {
            nome: nome,
            email: email,
            senha: senha,
            StatusConta: StatusConta,
            idTipo: idTipo
        }

        axios.post('http://localhost:5000/api/Usuario', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                console.log(resposta)
            }
        }).catch(erro => console.log(erro))
    }

    useEffect(listar_tipo, [])

    return (
        <section className='container' id='atualizar'>
            <div className='banner'>
                <img className='banner_login' src={banner} alt="Imagem para a tela de login" />
            </div>

            <div className='login'>
                <div className='container_cadastro'>
                    <h1 className='titulo_2'>Cadastrar</h1>
                    <form className='form_cadastro' onSubmit={(event) => Cadastrar(event)}>
                        <input type="text" id='Nome' name='Nome' placeholder='Nome' className='input_cadastro' value={nome} onChange={(e) => setNome(e.target.value)} />
                        <select name="idTipo" value={idTipo} onChange={(e) => setIdTipo(e.target.value)} className='input_cadastro'>
                                <option value="0" selected disable> Selecione o Tipo de Usuario</option>
                            {
                                listatipos.map((idtipo) => {
                                    return (
                                        <option key={idtipo.id} value={idtipo.id}>
                                            {idtipo.nomeTipo}
                                        </option>
                                    )
                                })
                            }
                        </select>
                        <input type="email" placeholder='Email:' id='Email' name='Email' className='input_cadastro' value={email} onChange={(e) => setEmail(e.target.value)} />
                        <input type="password" placeholder='Senha:' id='Senha' name='Senha' className='input_cadastro' onChange={(e) => setSenha(e.target.value)} />
                        <div className='container_inputs'>
                            <div className='box_atividade'>
                                <label className='nome_input'>Ativo:</label>
                                <input type="checkbox" placeholder='Ativo:' name='StatusConta' className='atividade' id="StatusConta"
                                    value={StatusConta} onChange={(e) => setStatusConta(e.target.value)} />
                            </div>
                        </div>
                        <span className="Mensagem_erro"></span>
                        <button className="button-cadastro" type='submit' >Cadastrar</button>
                    </form>
                </div>
            </div>
        </section>
    )
}