import React, {createContext, useState} from 'react';
import { Switch } from 'react-router';
import Layout from './components/layout/Layout';

import './custom.css'
import CatalogPage from "./pages/catalog/CatalogPage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import AuthorizedRoute from "./components/AuthorizedRoute";
import UnauthorizedRoute from "./components/UnauthorizedRoute";
import ProfilePage from "./pages/profile/ProfilePage";

export const AuthContext = createContext({
    user: null,
    setUser: () => {},
});

const App = () => {
    const [user, setUser] = useState(null);

    return (
        <AuthContext.Provider value={{ user, setUser }}>
            <Layout>
                <Switch>
                    <AuthorizedRoute exact path='/catalog' component={CatalogPage} />
                    <AuthorizedRoute exact path='/profile' component={ProfilePage} />
                    <UnauthorizedRoute exact path='/login' component={LoginPage} />
                    <UnauthorizedRoute exact path='/register' component={RegisterPage} />
                </Switch>
            </Layout>
        </AuthContext.Provider>
    )
}

export default App;