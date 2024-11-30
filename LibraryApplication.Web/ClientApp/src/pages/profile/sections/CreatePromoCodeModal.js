import React from "react";
import { API } from "../../../configs/axios.config";
import { Button, Form, Input, InputNumber, Modal, notification } from "antd";

const CreatePromoCodeModal = ({ setShowModal, getPromoCodes }) => {
  const onFinish = async (values) => {
    try {
      await API.post(`/api/Discount/create`, {
        name: values.promoCode,
        amount: values.discountPercent,
      });
      await getPromoCodes();
      setShowModal(false);
      notification.success({ message: "Promo code created!" });
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <Modal
      title="Create promo code"
      open={true}
      onCancel={() => setShowModal(false)}
      footer={null}
    >
      <Form
        name="promo-code"
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        requiredMark={false}
        layout="vertical"
      >
        <Form.Item
          name="promoCode"
          label="Promo code"
          rules={[
            {
              required: true,
              message: "Please input promo code!",
            },
          ]}
        >
          <Input placeholder="Promo code" />
        </Form.Item>
        <Form.Item
          name="discountPercent"
          label="Discount percent"
          rules={[
            {
              required: true,
              message: "Please input discount percent!",
            },
          ]}
          initialValue={10}
        >
          <InputNumber prefix="%" />
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

export default CreatePromoCodeModal;
