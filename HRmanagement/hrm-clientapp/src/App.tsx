import React from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
import Home from './components/Home';
import Navbar from './components/Navbar';
import Details from './components/Details';
import "react-bootstrap/dist/react-bootstrap.min.js";
import "./App.css";
import MemberDetail from './components/MemberDetail';

const App = () => {
  return (
    <div className="App">
      <Navbar></Navbar>
      <Router>
        <Switch>
          <Route exact path="/">
            <Home></Home>
          </Route>

          <Route exact path="/details">
            <Details></Details>
          </Route>

          <Route path="/memberdetail">
            <MemberDetail></MemberDetail>
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
