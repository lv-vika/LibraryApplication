import React from "react";
import { API } from "../../../configs/axios.config";
import { Button, Form, Input, InputNumber, Modal, notification } from "antd";

const CreateDamageFineModal = ({ setShowModal }) => {
  const onFinish = async (values) => {
    try {
      await API.post(`/api/Admin/generate-damage-fines`, {
        bookId: values.bookId,
        amount: values.amount,
      });
      setShowModal(false);
      notification.success({ message: "Fine generated!" });
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Modal
      title="Create damage fine"
      open={true}
      onCancel={() => setShowModal(false)}
      footer={null}
    >
      <Form
        name="fine"
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        requiredMark={false}
        layout="vertical"
      >
        <Form.Item
          name="bookId"
          label="Book Id"
          rules={[
            {
              required: true,
              message: "Please input book id!",
            },
          ]}
          initialValue={0}
        >
          <InputNumber />
        </Form.Item>
        <Form.Item
          name="amount"
          label="Amount $"
          rules={[
            {
              required: true,
              message: "Please input amount!",
            },
          ]}
          initialValue={10}
        >
          <InputNumber prefix="$" />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            Create
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateDamageFineModal;
