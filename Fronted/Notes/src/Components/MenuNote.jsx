import { DeleteIcon, EditIcon, HamburgerIcon } from "@chakra-ui/icons";
import { IconButton, Menu, MenuButton, MenuItem, MenuList, useDisclosure } from "@chakra-ui/react";
import UpdateNoteModal from "./UpdateNoteModal";

export default function MenuNote({note, onUpdate, onDelete}) {
    const { isOpen, onOpen, onClose } = useDisclosure();
    return (
        <>
            <Menu>
                <MenuButton
                    bg={'purple.200'}
                    as={IconButton}
                    aria-label='Options'
                    icon={<HamburgerIcon />}
                    _hover={{ bg: "purple.300" }}
                    _active={{ bg: 'purple.400', color: 'purple' }} />
                <MenuList>
                    <MenuItem icon={<EditIcon />} onClick={onOpen}>Редактировать</MenuItem>
                    <MenuItem icon={<DeleteIcon />} onClick={() => onDelete(note.id)}>Удалить</MenuItem>
                </MenuList>
            </Menu>
            {isOpen && (
                <UpdateNoteModal
                    isOpen={isOpen}
                    onClose={onClose}
                    note={note}
                    onUpdate={onUpdate}
                />
            )}
        </>
    )
}