import React from "react";
import { getDecodedUser } from "../../utils/tokenUtils";

const UserInfo: React.FC = () => {
  const userData = getDecodedUser();

  if (!userData) return null;

  return (
    <div
      style={{
        marginTop: "2rem",
        borderTop: "1px solid #ccc",
        paddingTop: "1rem",
      }}
    >
      <h3>User Info from JWT</h3>
      <p>
        <strong>Name:</strong> {userData.name}
      </p>
      <p>
        <strong>Email:</strong> {userData.email}
      </p>
      <p>
        <strong>Account ID:</strong> {userData.accountId}
      </p>
      <p>
        <strong>Role:</strong> {userData.role}
      </p>
    </div>
  );
};

export default UserInfo;
