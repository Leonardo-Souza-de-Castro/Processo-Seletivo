import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';

import {
  Route,
  BrowserRouter as Router,
  Routes,
} from 'react-router-dom';

import './index.css';
import Login from './pages/login';
import Perfil from './pages/Perfil';
import NotFound from './pages/NotFound';
import Cadastro from './pages/Cadastro';

const routing = (
  <Router>
    <div>
      <Routes>
        <Route exact path="/" element={<Login />} /> {/* Login */}
        <Route path="/perfil" element={<Perfil />} /> {/* Painel Adiministrativo */}
        <Route path="/cadastro" element={<Cadastro />} /> {/* Painel Adiministrativo */}
        <Route path="*" element={<NotFound/> } />
      </Routes>
    </div>
  </Router>
)

ReactDOM.render(
  routing, document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();