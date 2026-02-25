import authConfig from '../../auth_config.json';
export const environment = { production: false, auth0: { domain: authConfig.domain, clientId: authConfig.clientId } };
