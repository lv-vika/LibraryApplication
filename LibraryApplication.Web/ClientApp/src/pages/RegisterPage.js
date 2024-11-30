import React from 'react';
import {Button, Card, Col, Form, Input, notification, Row} from "antd";
import {Link, useHistory} from "react-router-dom";
import {API} from "../configs/axios.config";

const RegisterPage = () => {
    const history = useHistory()

    const onFinish = async values => {
        try {
            const {data: userId} = await API.post('/api/User/create', {
                login: values.login,
                password: values.password,
                name: values.name,
                surname: values.surname,
                address: values.address,
                registerDate: new Date().toISOString().slice(0, 23),
            })
            notification.success({message: 'User created!'})
            history.push('/login')
        } catch (e) {
            console.log(e)
        }
    }

    return (
        <Row align="middle" className='mt-xxl-5'>
            <Col span={12} offset={6}>
                <Card title="Register">
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
                            name="name"
                            label="Name"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your name!',
                                },
                            ]}
                        >
                            <Input placeholder="Name" />
                        </Form.Item>
                        <Form.Item
                            name="surname"
                            label="Surname"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your surname!',
                                },
                            ]}
                        >
                            <Input placeholder="Surname" />
                        </Form.Item>
                        <Form.Item
                            name="address"
                            label="Address"
                            rules={[
                                {
                                    required: true,
                                    message: 'Please input your address!',
                                },
                            ]}
                        >
                            <Input placeholder="Address" />
                        </Form.Item>
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
                                Register
                            </Button>
                                Or <Link to="/login">log in</Link>
                        </Form.Item>
                    </Form>
                </Card>
            </Col>
        </Row>
    );
};

export default RegisterPage;