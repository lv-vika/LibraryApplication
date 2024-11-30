import React, { useState } from "react";
import CreateDamageFineModal from "./CreateDamageFineModal";
import GenerateFineModal from "./GenerateFineModal";
import { Button } from "antd";
import { PlusOutlined } from "@ant-design/icons";

const Fines = () => {
  const [showGenerateFinesModal, setShowGenerateFinesModal] = useState(false);
  const [showCreateDamageFineModal, setShowCreateDamageFineModal] =
    useState(false);

  return (
    <>
      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <Button
          className="mb-lg-4"
          onClick={() => setShowGenerateFinesModal(true)}
          type="primary"
        >
          <PlusOutlined /> Generate past fines
        </Button>
        <Button
          className="mb-lg-4"
          onClick={() => setShowCreateDamageFineModal(true)}
          type="primary"
        >
          <PlusOutlined /> Create damage fine
        </Button>
      </div>
      {showGenerateFinesModal && (
        <GenerateFineModal setShowModal={setShowGenerateFinesModal} />
      )}
      {showCreateDamageFineModal && (
        <CreateDamageFineModal setShowModal={setShowCreateDamageFineModal} />
      )}
    </>
  );
};

export default Fines;
