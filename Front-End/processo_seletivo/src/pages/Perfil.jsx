import { useEffect, useState } from 'react';
import axios from 'axios'
import '../assets/css/Perfil.css'
import '../assets/css/Cadastro.css'
import { parsejwt } from '../services/Auth';

import img_perfil from '../assets/img/foto_perfil.svg'
import lapis from '../assets/img/lapis.svg'
import lixeira from '../assets/img/lixeira.svg'
import sair from '../assets/img/sair.svg'
import user from '../assets/img/user.svg'
import check from '../assets/img/verifica.svg'
import banner from '../assets/img/banner_cadastro.svg'

export default function Perfil() {
    const [listausers, setListausers] = useState([])
    const [nome, setNome] = useState('')
    const [senha, setSenha] = useState('')
    const [StatusConta, setStatusConta] = useState(true)
    const [email, setEmail] = useState('')
    const [idTipo, setIdTipo] = useState('')
    const [idUsuario, setIdUsuario] = useState(0)

    const Atualizar = (idUsuario) => {
        setIdUsuario(idUsuario)
        var conteudo = document.getElementById("perfil");
        var conteudo_perfil = document.getElementById("atualizar");
        if (conteudo.style.display === "flex") {
            conteudo_perfil.style.display = "none"
        } else {
            conteudo_perfil.style.display = "flex"
            conteudo.style.display = "none"
        }
    }

    const Atualiza_user = (event) => {
        event.preventDefault();
        let user_atualizado = {
            nome: nome,
            email: email,
            senha: senha,
            StatusConta: StatusConta,
        }
        console.log(idUsuario)
        axios('http://localhost:5000/api/Usuario/' + idUsuario, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                setNome(resposta.data.nome)
                setEmail(resposta.data.email)
                setIdTipo(resposta.data.idTipoNavigation)
                setStatusConta(resposta.data.StatusConta)
                axios.patch('http://localhost:5000/api/Usuario/' + idUsuario, user_atualizado, {
                    headers: {
                        'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
                    }
                }).then((resposta) => {
                    if (resposta.status === 200) {
                        console.log("SOU FODA")
                    }
                }).catch(erro => console.log(erro))
            }
        }).catch(erro => console.log(erro))
    }

    const atualizastate = (idUsuario) => {
        setIdUsuario(idUsuario)
        axios.delete('http://localhost:5000/api/Usuario/' + idUsuario, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                console.log('funciona')
            }
        }).catch(erro => console.log(erro))
    }

    const Listar = () => {
        axios('http://localhost:5000/api/Usuario', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                console.log(resposta)
                setListausers(resposta.data)
            }
        }).catch(erro => console.log(erro))
    }

    const Buscar = () => {
        axios('http://localhost:5000/api/Usuario/' + (parsejwt().jti), {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }).then((resposta) => {
            if (resposta.status === 200) {
                setNome(resposta.data.nome)
                setEmail(resposta.data.email)
                setIdTipo(resposta.data.idTipoNavigation)
                setStatusConta(resposta.data.StatusConta)
            }
        }).catch(erro => console.log(erro))
    }

    useEffect(Buscar, [])
    useEffect(Listar, [])

    return (
        <div>
            <section className='container_perfil' id='perfil'>
                <div className="menu">
                    <div className='itens_menu'>
                        <div className='iten'>
                            <hr className="barra_menu" />
                            <img src={user} alt="Icone para tela de perfil" />
                            <span className='opc_menu'>Perfil</span>
                        </div>
                        <div className='iten'>
                            <img src={sair} alt="Icone para sair" />
                            <span className='opc_menu'>Sair</span>
                        </div>
                    </div>
                </div>
                <div className="conteudo">
                    <section className="info_perfil">
                        <img src={img_perfil} alt="imagem de perfil" className='foto_perfil_user' />
                        <div className='box_infos_perfil'>
                            <span className='nome'>{nome}</span>
                            <div className="campos">
                                {
                                    idTipo.nomeTipo === "geral" ? <span className='info_principal'>Geral</span> : idTipo.nomeTipo === "admin" ? <span className='info_principal'>Administrador</span> : <span className='info_principal'>Root</span>
                                }

                                <div className='status'>
                                    <span className='info_principal'>status:</span>
                                    {
                                        StatusConta === true ? <img src={check} alt="Status do perfil" /> : <img src={check} alt="Status do Perfil" />
                                    }
                                </div>
                            </div>
                            <span className='info_principal'>{email}</span>
                        </div>
                    </section>
                    {
                        parsejwt().roles === 2 || 3 ?
                            <div className="outros_perfis">

                                {
                                    listausers.map((user) => {
                                        return (
                                            <div className='info_outros_perfis' key={user.idUsuario}>
                                                <div className='box_infos'>
                                                    <div className='box_foto'>
                                                        <hr />
                                                        <img src={img_perfil} alt="" className='foto_perfil_outro' />
                                                    </div>
                                                    <div className='info'>
                                                        <span className='info_outro_user'>{user.nome}</span>
                                                        <span className='info_outro_user'>{user.email}</span>
                                                    </div>
                                                </div>
                                                <div className='funcoes'>
                                                    <img src={lapis} alt="" onClick={() => Atualizar(user.idUsuario)} />
                                                    <img src={lixeira} alt="" onClick={() => atualizastate(user.idUsuario)} />
                                                </div>
                                            </div>
                                        )
                                    })
                                }

                            </div> : <div>
                            </div>
                    }
                </div>
            </section>

            <section className='container' id='atualizar'>
                <div className='banner'>
                    <img className='banner_login' src={banner} alt="Imagem para a tela de login" />
                </div>

                <div className='login'>
                    <div className='container_cadastro'>
                        <h1 className='titulo_2'>Atualizar</h1>
                        <form className='form_cadastro' onSubmit={(event) => Atualiza_user(event)}>
                            <input type="text" id='Nome' name='Nome' placeholder='Nome' className='input_cadastro' value={nome} onChange={(e) => setNome(e.target.value)} />


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
                            <button className="button-cadastro" type='submit'>Atualizar</button>
                        </form>
                    </div>
                </div>
            </section>

        </div >



    )
}