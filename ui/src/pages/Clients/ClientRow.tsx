import { TableCell, TableRow } from "@mui/material";
import { useNavigate } from "react-router-dom";

export interface IProps {
  client: IClient;
}

export default function ClientListItem({ client }: IProps) {
  const { id, firstName, lastName, email, phoneNumber } = client;

  const navigate = useNavigate();

  return (
    <TableRow
      key={id}
      sx={{
        "&:last-child td, &:last-child th": { border: 0 },
        cursor: "pointer",
        "&:hover": {
          backgroundColor: "#f5f5f5",
        },
      }}
    >
      <TableCell component="th" scope="row">
        {firstName} {lastName}
      </TableCell>
      <TableCell>{phoneNumber}</TableCell>
      <TableCell>{email}</TableCell>
    </TableRow>
  );
}
