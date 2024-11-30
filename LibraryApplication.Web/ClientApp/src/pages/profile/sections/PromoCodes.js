import React, { useEffect, useState } from "react";
import { Button, notification, Table } from "antd";
import { DeleteOutlined, PlusOutlined } from "@ant-design/icons";
import { API } from "../../../configs/axios.config";
import Title from "antd/es/typography/Title";
import CreatePromoCodeModal from "./CreatePromoCodeModal";

const PromoCodes = () => {
  const [showCreatePromoCodeModal, setShowCreatePromoCodeModal] =
    useState(false);
  const [promoCodes, setPromoCodes] = useState([]);
  const [loading, setLoading] = useState(false);

  const getPromoCodes = async () => {
    const { data: newPromoCodes } = await API.get(`/api/Discount/all`);
    setPromoCodes(newPromoCodes);
  };

  useEffect(() => {
    (async () => {
      setLoading(true);
      try {
        await getPromoCodes();
      } catch (e) {
        console.log(e);
      }
      setLoading(false);
    })();
  }, []);

  const columns = [
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Discount (%)",
      dataIndex: "discountPercent",
      key: "discountPercent",
    },
    {
      title: "Action",
      key: "action",
      width: "8%",
      render: (promoCode) => {
        const handleDeletePromoCode = async () => {
          try {
            const isSuccess = await API.delete(
              `/api/Discount/${promoCode.key}/delete`
            );
            if (isSuccess.data === true) {
              notification.success({ message: "Promo code deleted!" });
              await getPromoCodes();
            } else {
              notification.error({ message: "Failed to delete promo code!" });
            }
          } catch (error) {
            console.log(error);
          }
        };

        return (
          <Button shape="circle" type="text" onClick={handleDeletePromoCode}>
            <DeleteOutlined />
          </Button>
        );
      },
    },
  ];

  const data = promoCodes.map((promoCode) => {
    return {
      key: promoCode.id,
      name: promoCode.name,
      discountPercent: promoCode.amount,
    };
  });

  return (
    <>
      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <Title level={2}>Promo codes</Title>
        <Button
          className="mb-lg-4"
          onClick={() => setShowCreatePromoCodeModal(true)}
          type="primary"
        >
          <PlusOutlined /> Create promo code
        </Button>
      </div>
      <Table
        columns={columns}
        dataSource={data}
        pagination={false}
        loading={loading}
      />
      {showCreatePromoCodeModal && (
        <CreatePromoCodeModal
          setShowModal={setShowCreatePromoCodeModal}
          getPromoCodes={getPromoCodes}
        />
      )}
    </>
  );
};

export default PromoCodes;
