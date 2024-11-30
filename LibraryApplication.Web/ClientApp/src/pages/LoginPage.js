import React from 'react';
import {Button, Card, Col, Form, Input, Row} from "antd";
import {API} from "../configs/axios.config";
import {Link, useHistory} from "react-router-dom";
import {useAuth} from "../hooks/useAuth";

const LoginPage = () => {
    const history = useHistory()
    const { login } = useAuth();
    const onFinish = async values => {
        const {data: userId} = await API.post('/api/User', {
            login: values.login,
            password: values.password
        })
        const {data: user} = await API.get(`/api/User/${userId}`)
        login(user)
        history.push('/catalog')
    };

    return (
        <Row align="middle" className='mt-xxl-5'>
            <Col span={12} offset={6}>
                <Card title="Log in">
                    <Form
                        name="login"
                        initialValues={{
                            remember: true,
                        }}
                        onFinish={onFinish}
                        requiredMark={false}
                        layout="vertical"
                    >
                        <Form.Item
                            name="login"
                            label="Login"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your login!',
                                },
                            ]}
                        >
                            <Input placeholder="Login" />
                        </Form.Item>
                        <Form.Item
                            name="password"
                            label="Password"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your password!',
                                },
                            ]}
                        >
                            <Input
                                type="password"
                                placeholder="Password"
                            />
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" htmlType="submit">
                                Log in
                            </Button>
                            Or <Link to="/register">register now</Link>
                        </Form.Item>
                    </Form>
                </Card>
            </Col>
        </Row>
    );
};

export default LoginPage;