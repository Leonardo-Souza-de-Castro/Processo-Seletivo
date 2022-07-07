import { Component } from "react";
import axios from 'axios';
import { parsejwt } from '../services/Auth';

import '../assets/css/App.css';
import banner from '../assets/img/banner-login.svg';

export default class Login extends Component {
    constructor(props) {
        super(props)
        this.state = {
            email: '',
            senha: '',
            isLoading: false,
            MensagemErro: '',
        }
    }

    efetuarlogin = (evento) => {
        evento.preventDefault();
        this.setState({ mensagemErro: '', isLoading: true });
        console.log(this.state.email, this.state.senha);
        axios.post('http://localhost:5000/api/Login', {
            email: this.state.email,
            senha: this.state.senha
        }).then(resposta => {
            console.log(resposta)
            if (resposta.status === 201) {
                localStorage.setItem('usuario-login', resposta.data.token);
                this.setState({ isLoading: false })
                switch (parsejwt().role) {
                    case '1':
                        window.location.href = "../Perfil"
                        break;
                    case '2':
                        window.location.href = "../Perfil"
                        break;
                    case '3':
                        window.location.href = "../Perfil"
                        break;
                    default:
                        console.log('nn vai')
                        break;
                }
            }

        }).catch(() => {
            this.setState({
                MensagemErro: 'Email ou senha invalido',
                isLoading: false,
                email: '',
                senha: ''
            }, console.log('deu errado'))
        })
    }

    atualizaStateCampo = (campo) => {
        this.setState({ [campo.target.name]: campo.target.value });
    }

    render() {
        return (
            <section className='container'>
                <div className='banner'>
                    <img className='banner_login' src={banner} alt="Imagem para a tela de login" />
                </div>

                <div className='login'>
                    <div className='container_login'>
                        <div>
                            <h1 className='titulo'>Login</h1>
                            <h2 className='sub_titulo'>Acesse e veja seu perfil</h2>
                        </div>
                        <form className='form-login' onSubmit={this.efetuarlogin}>
                            <input type="email" placeholder='Email:' className='input_login' name="email" value={this.state.email} onChange={this.atualizaStateCampo} />
                            <input type="password" placeholder='Senha:' className='input_login' name="senha" value={this.state.senha} onChange={this.atualizaStateCampo} />
                            <span className="Mensagem_erro">{this.state.MensagemErro}</span>
                            {
                                this.state.isLoading === true ? <button disabled className="button-login">Entrando ...</button> : <button type='submit' className="button-login">Entrar</button>
                            }
                        </form>
                    </div>
                </div>
            </section>
        )
    }
}