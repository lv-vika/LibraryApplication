import React from "react";
import RentedBooks from "./sections/RentedBooks";
import { useUser } from "../../hooks/useUser";
import PromoCodes from "./sections/PromoCodes";
import Fines from "./sections/Fines";

const ProfilePage = () => {
  const { user } = useUser();

  return (
    <div>
      <RentedBooks />
      {user?.isAdmin && <PromoCodes />}
      {user?.isAdmin && <Fines />}
    </div>
  );
};

export default ProfilePage;
