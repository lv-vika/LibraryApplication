import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import {useUser} from "../hooks/useUser";

const UnauthorizedRoute = ({ component: Component, ...rest }) => {
    const { user } = useUser();

    return (
        <Route
            {...rest}
            render={(props) =>
                user ? <Redirect to="/catalog" /> : <Component {...props} />
            }
        />
    );
};

export default UnauthorizedRoute;
