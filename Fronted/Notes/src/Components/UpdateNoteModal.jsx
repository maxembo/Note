import { Button, FormControl, FormLabel, Input, Modal, ModalBody, ModalCloseButton, ModalContent, ModalFooter, ModalHeader, ModalOverlay, Textarea } from "@chakra-ui/react";
import { useEffect, useState } from "react";

export default function UpdateNoteModal({ isOpen, onClose, note, onUpdate }) {
    const [newNote, setNewNote] = useState({ title: note.title, description: note.description});

    // useEffect(() => {
    //     setNewNote({ 
    //       title: note.title, 
    //       description: note.description 
    //     });
    //   }, [note]);

    const handleUpdate = () => {
        onUpdate({...note, ...newNote});
        onClose();
    };

    return (
        <Modal
            size='4xl'
            isOpen={isOpen}
            onClose={onClose}
        >
            <ModalOverlay
                bg='whiteAlpha'
                backdropFilter='blur(10px)'
            />
            <ModalContent>
                <ModalHeader>Обновить заметку</ModalHeader>
                <ModalCloseButton />
                <ModalBody pb={6}>
                    <FormControl>
                        <FormLabel>Название</FormLabel>
                        <Input
                            placeholder='Название'
                            value={newNote.title}
                            onChange={(e) => setNewNote({ ...newNote, title: e.target.value })}
                        />
                    </FormControl>
                    <FormControl mt={4}>
                        <FormLabel>Описание</FormLabel>
                        <Textarea
                            placeholder='Описание'
                            value={newNote.description}
                            onChange={(e) => setNewNote({ ...newNote, description: e.target.value })}
                        />
                    </FormControl>
                </ModalBody>

                <ModalFooter>
                    <Button colorScheme='purple' mr={3} onClick={handleUpdate}>
                        Save
                    </Button>
                    <Button onClick={onClose}>Cancel</Button>
                </ModalFooter>
            </ModalContent>

        </Modal>

    )
}