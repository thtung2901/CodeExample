import * as localforage from "localforage";
import { useEffect, useState } from "react";
import { v4 as uuidv4 } from 'uuid';
import {
    checkInWithPushNotificationServer,
    getPlatformPermissionState,
    unsubscribeAllWalletsFromPushNotification,
} from 'utils/pushNotification';

interface PushNotificationConfig {
    allowPushNotification: boolean | undefined;
    appId: string;
    lastPushMessageTimestamp: number | undefined;
}

const KEY = 'pushNotificationConfig';

const loadPushNotificationConfigFromStorage = async (): Promise<PushNotificationConfig | null> => {
    try {
        return await localforage.getItem<PushNotificationConfig | null>(KEY);
    } catch (error) {
        console.error('ERROR in loadPushNotificationConfigFromStorage() in usePushNotification()');
        // TODO: log the error
        throw error;
    }
}

const savePushNotificationConfigToStorage = async (config: PushNotificationConfig): Promise<void> => {
    try {
        return await localforage.setItem(KEY, config);
    } catch (error) {
        console.error('ERROR in savePushNotificationConfigToStorage() in usePushNotification()');
        // TODO: log the error
        throw error;
    }
}

const usePushNotification = () => {
    const [pushNotification, setPushNotification] = useState<PushNotificationConfig | null>(null);

    useEffect(() => {
        const initializePushNotification = async () => {
            if ('serviceWorker' in navigator && 'PushManager' in window && 'Notification' in window) {
                let pushConfiguration = await loadPushNotificationConfigFromStorage();
                if (!pushConfiguration) {
                    // no configuration saved on local storage
                    // generate a new one and save it to local storage
                    pushConfiguration = {
                        allowPushNotification: undefined,
                        appId: uuidv4(),
                        lastPushMessageTimestamp: undefined,
                    }
                } else {
                    const permission = getPlatformPermissionState();
                    if (permission !== 'granted' && pushConfiguration.allowPushNotification) {
                        unsubscribeAllWalletsFromPushNotification(pushConfiguration);
                        pushConfiguration.allowPushNotification = false;
                    }
                }
                setPushNotification(pushConfiguration);
                checkInWithPushNotificationServer(pushConfiguration);
            }
        };

        initializePushNotification();

        return () => {
            // Cleanup if needed
        }
    }, []);

    useEffect(() => {
        if (pushNotification) {
            savePushNotificationConfigToStorage(pushNotification);
        }
    }, [pushNotification]);

    const turnOffPushNotification = () => {
        if (pushNotification) {
            setPushNotification({ ...pushNotification, allowPushNotification: false });
        }
    };

    const turnOnPushNotification = () => {
        if (pushNotification) {
            setPushNotification({ ...pushNotification, allowPushNotification: true });
        }
    };

    if (!pushNotification) return null;

    return {
        ...pushNotification,
        turnOffPushNotification,
        turnOnPushNotification,
    }
}

export default usePushNotification;
