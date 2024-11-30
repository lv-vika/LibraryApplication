import React, { useState } from "react";
import {
  Button,
  Form,
  Input,
  InputNumber,
  Modal,
  notification,
  Space,
} from "antd";
import { API } from "../../configs/axios.config";
import { useUser } from "../../hooks/useUser";
import { useAuth } from "../../hooks/useAuth";

const BorrowBookModal = ({ book, setShowModal, getBooks }) => {
  const { user } = useUser();
  const { login } = useAuth();

  const [promo, setPromo] = useState("");
  const [promoId, setPromoId] = useState(0);

  const onFinish = async (values) => {
    try {
      const isSuccess = await API.post(`/api/Book/${book.id}/borrow`, {
        userId: user?.id,
        rentInDays: values.rentInDays,
        discountId: !!promoId ? promoId : null,
      });
      const { data: newUser } = await API.get(`/api/User/${user?.id}`);
      login(newUser);

      isSuccess.data === true
        ? notification.success({ message: "Book borrowed!" })
        : notification.error({ message: "Failed to borrow book!" });

      setShowModal(false);

      await getBooks();
    } catch (e) {
      console.log(e);
    }
  };

  const onApplyPromo = async (_) => {
    try {
      const response = await API.get(`/api/Discount?name=${promo}`);

      notification.success({ message: "Promo applied!" });

      setPromoId(response.data.id);
    } catch (e) {
      notification.error({ message: "Failed to apply promo!" });
      setPromo("");
      console.log(e);
    }
  };

  return (
    <Modal
      title="Add book"
      open={true}
      onCancel={() => setShowModal(false)}
      footer={null}
    >
      <Form
        name="borrow-book"
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        requiredMark={false}
        layout="vertical"
      >
        <Form.Item
          name="rentInDays"
          label="Rent in days"
          rules={[
            { required: true, message: "Please, enter days number of rent" },
          ]}
          initialValue={0}
        >
          <InputNumber />
        </Form.Item>
        <Form.Item name="promoCode" label="Promo code">
          <Space.Compact>
            <Input
              value={promo}
              onChange={(e) => setPromo(e.target.value)}
              placeholder="Promo code"
            />
            <Button type="primary" onClick={onApplyPromo}>
              Apply
            </Button>
          </Space.Compact>
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            Borrow
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default BorrowBookModal;
