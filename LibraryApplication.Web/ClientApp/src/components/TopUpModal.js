import React from 'react';
import {Button, Form, InputNumber, Modal} from "antd";
import {useUser} from "../hooks/useUser";
import {API} from "../configs/axios.config";
import {useAuth} from "../hooks/useAuth";

const TopUpModal = ({setShowModal}) => {
    const { user } = useUser();
    const { login } = useAuth();

    const onFinish = async (values) => {
        try {
            const newUser = {...user, balance: user?.balance + values.topUpAmount}
            await API.put(`/api/User/${user?.id}/edit`, newUser)
            login(newUser)
            setShowModal(false)
        } catch (e) {
            console.log(e)
        }
    }

    return (
        <Modal
            title="Top up"
            open={true}
            onCancel={() => setShowModal(false)}
            footer={null}
        >
            <Form
                name="top-up"
                initialValues={{
                    remember: true,
                }}
                onFinish={onFinish}
                requiredMark={false}
                layout="vertical"
            >
                <Form.Item
                    name="topUpAmount"
                    label="Top up amount"
                    rules={[
                        {
                            required: true,
                            message: 'Please input top up amount!'
                        },
                    ]}
                    initialValue={10}
                >
                    <InputNumber prefix="$" />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        Submit
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default TopUpModal;